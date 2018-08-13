using ScrapeCity.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class USBPort
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public USBPortEnum Type { get; set; }

        public int Num { get; set; }

        [JsonIgnore]
        public int MonitorId { get; set; }
    }
}
