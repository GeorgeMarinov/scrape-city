using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class CertificateStandartLicense
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Value { get; set; }

        [JsonIgnore]
        public int MonitorId { get; set; }
    }
}
