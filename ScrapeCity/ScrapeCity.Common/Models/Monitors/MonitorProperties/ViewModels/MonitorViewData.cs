using ScrapeCity.Common.Models.Brands.ViewModels;
using System.Collections.Generic;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{

    public class MonitorViewData
    {
        public MonitorViewData()
        {
            VideoPorts = new List<string>();
            Backlights = new List<string>();
            ScreenTypes = new List<string>();
            PanelTypes = new List<string>();
            DisplaySyncTypes = new List<string>();
            USBPorts = new List<string>();
            Brands = new List<BrandVm>();
        }

        public IEnumerable<string> USBPorts { get; set; }
        public IEnumerable<string> VideoPorts { get; set; }
        public IEnumerable<string> Backlights { get; set; }
        public IEnumerable<string> ScreenTypes { get; set; }
        public IEnumerable<string> PanelTypes { get; set; }
        public IEnumerable<string> DisplaySyncTypes { get; set; }
        public IEnumerable<BrandVm> Brands { get; set; }
    }
}