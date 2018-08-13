using AutoMapper;
using ScrapeCity.Common;
using ScrapeCity.Common.Enums;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Brands.ViewModels;
using ScrapeCity.Common.Models.ImageAddresses;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using ScrapeCity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace ScrapeCity.Domain.MonitorServices
{
    public class MonitorService : AbstractService, IMonitorService
    {
        public IEnumerable<Monitor> GetAllMonitors()
        {
            return context.Monitors.ToList();
        }

        public IEnumerable<Monitor> GetMonitorsByBrandId(int brandId)
        {
            return context.Monitors.Where(x => x.Brand.Id == brandId).ToList();
        }

        public MonitorViewData GetMonitorViewData()
        {
            var allBrands = context.Brands.ToList();
            var vms = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandVm>>(allBrands);

            VideoPortEnum[] videoPortEnums = (VideoPortEnum[])VideoPortEnum.GetValues(typeof(VideoPortEnum));
            var videoPortVm = videoPortEnums.Select(x => x.ToString());

            USBPortEnum[] USBPortEnums = (USBPortEnum[])USBPortEnum.GetValues(typeof(USBPortEnum));
            var USBPortVm = USBPortEnums.Select(x => x.ToString());

            Backlight[] backlightEnums = (Backlight[])Backlight.GetValues(typeof(Backlight));
            var backlightVm = backlightEnums.Select(x => x.ToString());

            ScreenType[] screenTypeEnums = (ScreenType[])ScreenType.GetValues(typeof(ScreenType));
            var screenTypeVm = screenTypeEnums.Select(x => x.ToString());

            PanelType[] panelTypeEnums = (PanelType[])PanelType.GetValues(typeof(PanelType));
            var panelTypeVm = panelTypeEnums.Select(x => x.ToString());

            DisplaySyncType[] displaySyncTypeEnums = (DisplaySyncType[])DisplaySyncType.GetValues(typeof(DisplaySyncType));
            var displaySyncTypeVm = displaySyncTypeEnums.Select(x => x.ToString());


            return new MonitorViewData
            {
                Brands = vms,
                DisplaySyncTypes = displaySyncTypeVm,
                VideoPorts = videoPortVm,
                USBPorts = USBPortVm,
                Backlights = backlightVm,
                ScreenTypes = screenTypeVm,
                PanelTypes = panelTypeVm
            };
        }

        public int AddMonitorToDb(MonitorVm bind)
        {
            var speakers = Mapper.Map<SpeakersVm, Speakers>(bind.Speakers);
            var camera = Mapper.Map<CameraVm, Camera>(bind.Camera);
            var monitor = Mapper.Map<MonitorVm, Monitor>(bind);
            var placeholderImage = @"/placeholder.jpg";

            foreach (var port in bind.VideoPorts)
            {
                var type = VideoPortEnum.VGA;
                Enum.TryParse(port.Type, out type);
                var videoPort = new VideoPort()
                {
                    Type = type,
                    Num = port.Num,
                };
                monitor.VideoPorts.Add(videoPort);
            }

            foreach (var usbPort in bind.USBPorts)
            {
                var type = USBPortEnum.usb_2_0;
                Enum.TryParse(usbPort.Type, out type);
                var USBPort = new USBPort()
                {
                    Type = type,
                    Num = usbPort.Num,
                };
                monitor.USBPorts.Add(USBPort);
            }

            foreach (var entry in bind.PanelColors)
            {
                monitor.PanelColors.Add(new PanelColor() { Value = entry });
            }

            foreach (var entry in bind.CertificatesStandartsLicenses)
            {
                monitor.CertificatesStandartsLicenses.Add(new CertificateStandartLicense() { Value = entry });
            }

            foreach (var entry in bind.Features)
            {
                monitor.Features.Add(new Feature() { Value = entry });
            }

            foreach (var entry in bind.UnhandledDisplaySpecificationProperties)
            {
                monitor.UnhandledDisplaySpecificationProperties.Add(new UnhandledDIsplaySpecificationProperty() { Value = entry });
            }

            monitor.Speakers = speakers;
            monitor.Camera = camera;
            monitor.Brand = context.Brands.FirstOrDefault(x => x.Id == bind.BrandId);
            monitor.Thumbnail = placeholderImage;
            context.Monitors.Add(monitor);
            context.SaveChanges();

            return monitor.Id;
        }

        public IEnumerable<AllMonitorsVm> Get10MonitorsOfEachBrand()
        {
            var brands = context.Brands.ToList();

            var vms = new List<AllMonitorsVm>();

            foreach (var brand in brands)
            {
                var monitorsByBrand = GetMonitorsByBrandId(brand.Id);

                var vm = new AllMonitorsVm()
                {
                    MonitorCount = monitorsByBrand.Count(),
                    Brand = brand.BrandName,
                    MonitorListVms = new List<MonitorListVm>()
                };

                foreach (var monitor in monitorsByBrand.Take(10))
                {
                    var monitorVm = Mapper.Map<Monitor, MonitorListVm>(monitor);

                    vm.MonitorListVms.Add(monitorVm);
                }

                vms.Add(vm);
            }
            return vms;
        }

        public IEnumerable<MonitorListVm> Get10MonitorsFromBrandWithOffset(string brand, int offset)
        {
            var monitors = context.Monitors.Where(x => x.Brand.BrandName == brand).ToList().Skip(offset).Take(10);
            var vms = Mapper.Map<IEnumerable<Monitor>, IEnumerable<MonitorListVm>>(monitors);
            return vms;
        }

        public bool Delete(int Id)
        {
            var monitorToDelete = context.Monitors.Find(Id);
            if (monitorToDelete != null)
            {
                context.Monitors.Remove(monitorToDelete);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public MonitorVm GetMonitorVm(int Id)
        {
            var monitor = context.Monitors.Include(x => x.Speakers).Include(x => x.Camera).FirstOrDefault(x=>x.Id == Id);
            var vm = Mapper.Map<Monitor, MonitorVm>(monitor);
            var imagesVm = GetMonitorPicturesAndThumbnail(Id);
            vm.MonitorPictures = imagesVm.MonitorPictures;
            vm.Thumbnail = imagesVm.Thumbnail;
            return vm;
        }

        public MonitorPicturesAndThumbnailVm GetMonitorPicturesAndThumbnail(int Id)
        {
            var monitorPictures = context.Monitors.Find(Id).MonitorImages;
            var thumbnail = context.Monitors.Find(Id).Thumbnail;
            var vm = new MonitorPicturesAndThumbnailVm();
            vm.Thumbnail = thumbnail;
            vm.MonitorPictures = Mapper.Map<ICollection<MonitorImageAddress>, List<MonitorPicturesVm>>(monitorPictures);
            return vm;
        }

        public MonitorVm GetEditMonitorVm(int Id)
        {
            var vm = GetMonitorVm(Id);
            var viewData = GetMonitorViewData();
            vm.ViewData = viewData;
            return vm;
        }

        public int EditMonitor(MonitorVm bind)
        {
            var monitor = context.Monitors.Find(bind.Id);

            if (monitor.Brand.Id != bind.BrandId)
            {
                monitor.Brand = context.Brands.FirstOrDefault(x => x.Id == bind.BrandId);
            }

            monitor = Mapper.Map<MonitorVm, Monitor>(bind, monitor);

            var obsoleteVideoPorts = context.VideoPorts.Where(x => x.MonitorId == bind.Id).ToList();
            var obsoleteUSBPorts = context.USBPorts.Where(x => x.MonitorId == bind.Id).ToList();
            var obsoletePanelColors = context.PanelColors.Where(x => x.MonitorId == bind.Id).ToList();
            var obsoleteFeatures = context.Features.Where(x => x.MonitorId == bind.Id).ToList();
            var obsoleteCsls = context.CertificatesStandartsLicenses.Where(x => x.MonitorId == bind.Id).ToList();
            var obsoleteUnhandled = context.UnhandledDisplaySpecificationProperties.Where(x => x.MonitorId == bind.Id).ToList();

            foreach (var port in obsoleteVideoPorts)
            {
                context.VideoPorts.Remove(port);
            }
            foreach (var usbPort in obsoleteUSBPorts)
            {
                context.USBPorts.Remove(usbPort);
            }
            foreach (var panelColor in obsoletePanelColors)
            {
                context.PanelColors.Remove(panelColor);
            }
            foreach (var feature in obsoleteFeatures)
            {
                context.Features.Remove(feature);
            }
            foreach (var csl in obsoleteCsls)
            {
                context.CertificatesStandartsLicenses.Remove(csl);
            }
            foreach (var unhandled in obsoleteUnhandled)
            {
                context.UnhandledDisplaySpecificationProperties.Remove(unhandled);
            }


            foreach (var port in bind.VideoPorts)
            {
                var type = VideoPortEnum.VGA;
                Enum.TryParse(port.Type, out type);
                var videoPort = new VideoPort()
                {
                    Type = type,
                    Num = port.Num,
                };
                monitor.VideoPorts.Add(videoPort);
            }

            foreach (var USBport in bind.USBPorts)
            {
                var type = USBPortEnum.usb_2_0;
                Enum.TryParse(USBport.Type, out type);
                var USBPort = new USBPort()
                {
                    Type = type,
                    Num = USBport.Num,
                };
                monitor.USBPorts.Add(USBPort);
            }

            foreach (var entry in bind.PanelColors)
            {
                monitor.PanelColors.Add(new PanelColor() { Value = entry });
            }

            foreach (var entry in bind.CertificatesStandartsLicenses)
            {
                monitor.CertificatesStandartsLicenses.Add(new CertificateStandartLicense() { Value = entry });
            }

            foreach (var entry in bind.Features)
            {
                monitor.Features.Add(new Feature() { Value = entry });
            }

            foreach (var entry in bind.UnhandledDisplaySpecificationProperties)
            {
                monitor.UnhandledDisplaySpecificationProperties.Add(new UnhandledDIsplaySpecificationProperty() { Value = entry });
            }

            context.Entry(monitor).State = EntityState.Modified;
            context.SaveChanges();

            return monitor.Id;
        }

        public void SaveImageToDb(int monitorId, string path)
        {
            var imageAddress = new MonitorImageAddress()
            {
                Uri = path
            };
            var monitor = context.Monitors.Find(monitorId);
            monitor.MonitorImages.Add(imageAddress);
            context.SaveChanges();
        }

        public void UpdateThumbnail(int monitorId, int imageId)
        {
            var monitor = context.Monitors.Find(monitorId);
            var image = monitor.MonitorImages.FirstOrDefault(x => x.Id == imageId);
            monitor.Thumbnail = image.Uri;
            context.SaveChanges();
        }

        public PublicWebsiteIndexSliderValues GetPublicWebsiteIndexSliderValues()
        {
            var monitors = GetAllMonitors();

            return new PublicWebsiteIndexSliderValues()
            {
                ScreenSizeMax = monitors.Max(x => x.ScreenSize),
                ScreenSizeMin = monitors.Min(x => x.ScreenSize),
            };
        }

        public void DeleteImage(int monitorId, int imageId, string appLocation)
        {
            var monitorUploadPath = Settings.ImagesServerUploadPath + "Monitor\\";
            var monitor = context.Monitors.Find(monitorId);
            var image = monitor.MonitorImages.FirstOrDefault(x => x.Id == imageId);

            var fileRelativePath = image.Uri.Substring(1).Replace("/", "\\");

            string hdddir = "";
            if (Path.IsPathRooted(monitorUploadPath))
            {
                hdddir = Path.Combine(monitorUploadPath, fileRelativePath);
            }
            else
            {
                hdddir = Path.Combine(appLocation, monitorUploadPath.Substring(3), fileRelativePath);
            }

            try
            {
                System.IO.File.Delete(hdddir);
            }
            catch (Exception)
            {
                throw;
            }
            context.MonitorImageAddresses.Remove(image);
            context.SaveChanges();
        }
    }
}
