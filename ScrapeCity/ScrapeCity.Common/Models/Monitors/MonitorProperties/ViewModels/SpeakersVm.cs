using System.ComponentModel.DataAnnotations;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class SpeakersVm
    {
        [Display(Name = "Does this monitor have speakers ?")]
        public bool HasSpeakers { get; set; }
        public int Quantity { get; set; }
        public int Watts { get; set; }
    }
}
