using ScrapeCity.Common.Models.Brands.ViewModels;
using ScrapeCity.Domain.Interfaces;
using System.Web.Mvc;

namespace ScrapeCity.AdminPanel.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private IBrandService service;

        public BrandController(IBrandService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var vms = service.GetAllBrandsVm();
            return View(vms);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BrandVm bind)
        {
            if (ModelState.IsValid)
            {
                service.AddBrandToDb(bind);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var vm = service.GetBrandById(Id);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(BrandVm bind)
        {
            if (ModelState.IsValid)
            {
                service.EditBrand(bind);
                return RedirectToAction("Index");
            }
            return View(bind);
        }

        public ActionResult Delete(int Id)
        {
            service.DeleteBrand(Id);
            return RedirectToAction("Index");
        }
    }
}