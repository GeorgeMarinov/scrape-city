using System.ComponentModel.DataAnnotations;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class VideoPortVm
    {
        [Display(Name = "Number of video ports of given type")]
        public int Num { get; set; }
        public string Type { get; set; }
    }
}