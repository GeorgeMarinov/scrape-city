using ScrapeCity.Common.Models.Monitors;
using System.Collections.Generic;

namespace ScrapeCity.Common.Models.ImageAddresses
{
    public class MonitorImageAddress
    {

        public int Id { get; set; }
        public string Uri { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
    }
}
