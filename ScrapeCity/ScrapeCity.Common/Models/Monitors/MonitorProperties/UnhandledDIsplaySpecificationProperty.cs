using Newtonsoft.Json;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties
{
    public class UnhandledDIsplaySpecificationProperty
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Value { get; set; }

        [JsonIgnore]
        public int MonitorId { get; set; }
    }
}
