using ScrapeCity.Common.Models.ResponeModels;
using ScrapeCity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using ScrapeCity.Common;
using System.Web;
using Hangfire;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using HeyRed.Mime;

namespace ScrapeCity.AdminPanel.Controllers
{
    [Authorize]
    public class MonitorController : Controller
    {
        private IMonitorService service;
        private IAlgoliaMonitorIndex index;

        public MonitorController(IMonitorService service, IAlgoliaMonitorIndex index)
        {
            this.service = service;
            this.index = index;
        }

        public ActionResult Index()
        {
            var viewModels = service.Get10MonitorsOfEachBrand();

            return View(viewModels);
        }

        [HttpPost]
        public JsonResult ShowMoreMonitors(string brand, int offset)
        {
            var vms = service.Get10MonitorsFromBrandWithOffset(brand, offset);
            return Json(vms);
        }

        public ActionResult Add()
        {
            return View(new MonitorVm()
            {
                ViewData = service.GetMonitorViewData()
            });
        }

        [HttpPost]
        public ActionResult Add(MonitorVm bind)
        {
            if (ModelState.IsValid)
            {
                var monitorId = service.AddMonitorToDb(bind);

                BackgroundJob.Enqueue(() => index.AddItem(monitorId));

                return RedirectToAction("Index");
            }

            bind.ViewData = service.GetMonitorViewData();

            return View(bind);
        }

        public ActionResult Delete(int Id)
        {
            if (service.Delete(Id))
            {
                BackgroundJob.Enqueue(() => index.DeleteItem(Id));

                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int Id)
        {
            var editMonitorVm = service.GetEditMonitorVm(Id);
            return View(editMonitorVm);
        }

        [HttpPost]
        public ActionResult Edit(MonitorVm bind)
        {
            if (ModelState.IsValid)
            {
                var monitorId = service.EditMonitor(bind);

                BackgroundJob.Enqueue(() => index.EditItem(monitorId));

                return RedirectToAction("Index");
            }
            var picturesVm = service.GetMonitorPicturesAndThumbnail(bind.Id);
            bind.Thumbnail = picturesVm.Thumbnail;
            bind.MonitorPictures = picturesVm.MonitorPictures;
            bind.ViewData = service.GetMonitorViewData();

            return View(bind);
        }

        public ActionResult Details(int Id)
        {
            var vm = service.GetMonitorVm(Id);
            return View(vm);
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            //TODO better response ?
            var monitorUploadPath = Settings.ImagesServerUploadPath + "Monitor\\";
            var responseForEachFile = new List<ResponseModel<string>>();
            var monitorId = int.Parse(Request.Form.GetValues("monitorId")[0]);
            var filesToUpload = Request.Files;
            var monitor = service.GetMonitorVm(monitorId);

            for (int i = 0; i < filesToUpload.Count; i++)
            {
                var response = new ResponseModel<string>();
                var currentFile = filesToUpload[i];
                //validation null or empty
                if (currentFile != null && currentFile.ContentLength > 0)
                {
                    //get type
                    var fileType = currentFile.ContentType;
                    //validation is image ?
                    if (fileType == "image/jpeg" || fileType == "image/png")
                    {
                        //validation size 
                        if (currentFile.ContentLength < (7 * 1024 * 1024))
                        {
                            response.IsSuccess = true;
                            //createfile name
                            var fileName = Guid.NewGuid().ToString() + ".jpg";

                            //for local development it's better to use relative paths, 
                            //so we are going to use ..\images\monitor instead of C:\ala\...\...\...\bala\images\monitor
                            //If the path contains C:\******* it means it's rooted, and we use the raw value from the settings.
                            //if it contaisn only "..\images\" it will not be rooted and we need to map the IIS directory to rooted path directory
                            string uploadDir = "";
                            if (Path.IsPathRooted(monitorUploadPath))
                            {
                                uploadDir = Path.Combine(monitorUploadPath, monitor.BrandName, monitor.Model);
                            }
                            else
                            {
                                uploadDir = Path.Combine(HttpContext.Server.MapPath("~"), monitorUploadPath, monitor.BrandName, monitor.Model);
                            }

                            Directory.CreateDirectory(uploadDir);
                            var imageUrlPath = $@"/{monitor.BrandName}/{monitor.Model}/{fileName}";
                            var uploadPath = Path.Combine(uploadDir, fileName);

                            //save to hdd
                            currentFile.SaveAs(uploadPath);

                            //save to db
                            service.SaveImageToDb(monitorId, imageUrlPath);
                        }
                        else
                        {
                            response.Errors.Add("Image cannot be larger than 7mb");
                        }
                    }
                    else
                    {
                        response.Errors.Add("You can only upload image files(jpeg/png)");
                    }
                }
                else
                {
                    response.Errors.Add("File is null or empty");
                }
                responseForEachFile.Add(response);
            }
            return new JsonResult()
            {
                Data = responseForEachFile
            };
        }

        [HttpPost]
        public JsonResult UploadImageLinks()
        {
            //TODO better response ?
            var responseForEachFile = new List<ResponseModel<string>>();
            var monitorId = int.Parse(Request.Form.GetValues("monitorId")[0]);
            var uploadImageLinksData = Request.Form.GetValues("uploadImageLink")[0];
            var uploadImageLinks = uploadImageLinksData.Split(new string[] { "uploadImageLink=" }, StringSplitOptions.RemoveEmptyEntries);

            var monitor = service.GetMonitorVm(monitorId);

            //use webclient to download files from links
            var wc = new WebClient();

            foreach (var link in uploadImageLinks)
            {
                var monitorUploadPath = Settings.ImagesServerUploadPath + "Monitor\\";
                var response = new ResponseModel<string>();
                //check if url is valid
                if (Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute))
                {
                    //download
                    var currentFile = wc.DownloadData(link);
                    //validation null or empty
                    if (currentFile != null && currentFile.Length > 0)
                    {
                        //guess type
                        MimeGuesser.MagicFilePath = HttpRuntime.BinDirectory + "\\magic.mgc";
                        var fileType = MimeGuesser.GuessMimeType(currentFile);
                        //validation is image ?
                        if (fileType == "image/jpeg" || fileType == "image/png")
                        {
                            //validation size 
                            if (currentFile.Length < (7 * 1024 * 1024))
                            {
                                response.IsSuccess = true;
                                //createfile name
                                var fileName = Guid.NewGuid().ToString() + ".jpg";

                                //for local development it's better to use relative paths, 
                                //so we are going to use ..\images\monitor instead of C:\ala\...\...\...\bala\images\monitor
                                //If the path contains C:\******* it means it's rooted, and we use the raw value from the settings.
                                //if it contaisn only "..\images\" it will not be rooted and we need to map the IIS directory to rooted path directory
                                string uploadDir = "";
                                if (Path.IsPathRooted(monitorUploadPath))
                                {
                                    uploadDir = Path.Combine(monitorUploadPath, monitor.BrandName, monitor.Model);
                                }
                                else
                                {
                                    uploadDir = Path.Combine(HttpContext.Server.MapPath("~"), monitorUploadPath, monitor.BrandName, monitor.Model);
                                }

                                Directory.CreateDirectory(uploadDir);
                                var imageUrlPath = $@"/{monitor.BrandName}/{monitor.Model}/{fileName}";
                                var uploadPath = Path.Combine(uploadDir, fileName);

                                //save to hdd
                                using (Image image = Image.FromStream(new MemoryStream(currentFile)))
                                {
                                    image.Save(uploadPath, ImageFormat.Jpeg);
                                }
                                //save to db
                                service.SaveImageToDb(monitorId, imageUrlPath);
                            }
                            else
                            {
                                response.Errors.Add("Image cannot be larger than 7mb");
                            }
                        }
                        else
                        {
                            response.Errors.Add("You can only upload image files(jpeg/png)");
                        }
                    }
                    else
                    {
                        response.Errors.Add("File is null or empty");
                    }
                }
                else
                {
                    response.Errors.Add("Bad Url");
                }
                responseForEachFile.Add(response);
            }
            return new JsonResult()
            {
                Data = responseForEachFile
            };
        }

        [HttpPost]
        public JsonResult UpdateThumbnail()
        {
            var response = new ResponseModel<string>();
            var monitorId = int.Parse(Request.Form.GetValues("monitorId")[0]);
            var imageId = int.Parse(Request.Form.GetValues("imageId")[0]);
            service.UpdateThumbnail(monitorId, imageId);

            return new JsonResult()
            {
                Data = response
            };
        }

        [HttpPost]
        public JsonResult DeleteImage()
        {
            var response = new ResponseModel<string>();
            var monitorId = int.Parse(Request.Form.GetValues("monitorId")[0]);
            var imageId = int.Parse(Request.Form.GetValues("imageId")[0]);
            var appLocation =  Directory.GetParent(HttpRuntime.AppDomainAppPath).Parent.FullName;

            service.DeleteImage(monitorId, imageId, appLocation);

            return new JsonResult()
            {
                Data = response
            };
        }

        public ActionResult UpdateMonitorIndex()
        {
            BackgroundJob.Enqueue(() => index.UpdateMonitorIndex());

            return Redirect(Url.Content("~/"));
        }
    }
}