using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ScrapeCity.Common.Enums;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class VideoPort
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VideoPortEnum Type { get; set; }

        public int Num { get; set; }

        [JsonIgnore]
        public int MonitorId { get; set; }
    }
}
