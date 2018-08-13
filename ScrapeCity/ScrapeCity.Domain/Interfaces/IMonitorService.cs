using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using System.Collections.Generic;

namespace ScrapeCity.Domain.Interfaces
{
    public interface IMonitorService
    {
        IEnumerable<Monitor> GetAllMonitors();
        IEnumerable<Monitor> GetMonitorsByBrandId(int brandId);
        MonitorViewData GetMonitorViewData();
        int AddMonitorToDb(MonitorVm bind);
        IEnumerable<AllMonitorsVm> Get10MonitorsOfEachBrand();
        IEnumerable<MonitorListVm> Get10MonitorsFromBrandWithOffset(string brand, int offset);
        MonitorVm GetMonitorVm(int Id);
        MonitorVm GetEditMonitorVm(int id);
        bool Delete(int Id);
        int EditMonitor(MonitorVm bind);
        void SaveImageToDb(int monitorId, string path);
        void UpdateThumbnail(int monitorId, int imageId);
        PublicWebsiteIndexSliderValues GetPublicWebsiteIndexSliderValues();
        MonitorPicturesAndThumbnailVm GetMonitorPicturesAndThumbnail(int Id);
        void DeleteImage(int monitorId, int imageId, string appLocation);
    }
}
