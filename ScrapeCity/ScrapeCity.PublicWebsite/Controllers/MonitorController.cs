using ScrapeCity.Common;
using ScrapeCity.Domain.Interfaces;
using System.Web.Mvc;

namespace ScrapeCity.PublicWebsite.Controllers
{
    public class MonitorController : Controller
    {
        private IMonitorService service;

        public MonitorController(IMonitorService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var viewData = service.GetPublicWebsiteIndexSliderValues();

            viewData.ImageDomain = Settings.ImagesDomain;
            viewData.AlgoliaAppId = Settings.AlgoliaAppId;
            viewData.AlgoliaApiKey = Settings.AlgoliaSearchApiKey;
            viewData.AlgoliaMonitorsIndex = Settings.AlgoliaMonitorsIndex;
            return View(viewData);
        }

        public ActionResult Details(int Id)
        {
            var vm = service.GetMonitorVm(Id);
            return View(vm);
        }
    }
}