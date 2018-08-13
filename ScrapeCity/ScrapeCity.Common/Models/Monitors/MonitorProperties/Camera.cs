using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class Camera
    {
        public Camera()
        {
            Monitors = new List<Monitor>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        public bool HasCamera { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ImageResolutionPixels { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ImageResolutionMegaPixels { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string VideoResolutionPixels { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double VideoResolutionMegaPixels { get; set; }

        [JsonIgnore]
        public virtual ICollection<Monitor> Monitors { get; set; }
    }
}
