using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class Speakers
    {
        public Speakers()
        {
            Monitors = new List<Monitor>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        public bool HasSpeakers { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Quantity { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Watts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Monitor> Monitors { get; set; }
    }
}
