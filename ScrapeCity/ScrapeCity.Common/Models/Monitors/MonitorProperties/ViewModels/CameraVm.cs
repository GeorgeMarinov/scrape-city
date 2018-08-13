using System.ComponentModel.DataAnnotations;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class CameraVm
    {
        [Display(Name = "Does this monitor have a camera ?")]
        public bool HasCamera { get; set; }
        [Display(Name = "Image resolution pixels")]
        public string ImageResolutionPixels { get; set; }
        [Display(Name = "Image resolution mega pixels")]
        public double ImageResolutionMegaPixels { get; set; }
        [Display(Name = "Video resolution pixels")]
        public string VideoResolutionPixels { get; set; }
        [Display(Name = "Video resolution mega pixels")]
        public double VideoResolutionMegaPixels { get; set; }
    }
}
