using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;

namespace ScrapeCity.Common.Models.Monitors.ViewModels
{
    public class MonitorListVm : ThumbnailVm
    {
        public int Id { get; set; }
        public string Model { get; set; }
    }
}