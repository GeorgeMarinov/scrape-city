using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.Monitors.ViewModels
{
    public class AllMonitorsVm
    {
        public int MonitorCount { get; set; }
        public string Brand { get; set; }
        public List<MonitorListVm> MonitorListVms { get; set; }
    }
}
