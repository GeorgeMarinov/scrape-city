using Algolia.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScrapeCity.Common;
using ScrapeCity.Data;
using ScrapeCity.Domain.Interfaces;
using System.Linq;

namespace ScrapeCity.AdminPanel.HangfireJobs
{
    public class AlgoliaMonitorIndex : IAlgoliaMonitorIndex
    {
        AlgoliaClient _client;
        Index _index;

        public AlgoliaMonitorIndex()
        {
            _client = new AlgoliaClient(Settings.AlgoliaAppId, Settings.AlgoliaAdminApiKey);
            _index = _client.InitIndex(Settings.AlgoliaMonitorsIndex);
        }

        public void AddItem(int Id)
        {
            using (var context = new ScrapeCityDbContext())
            {
                _index.AddObject(JObject.Parse(JsonConvert.SerializeObject(context.Monitors.Find(Id), Formatting.Indented, converters: new Newtonsoft.Json.Converters.StringEnumConverter())));
            }
        }

        public void DeleteItem(int Id)
        {
            _index.DeleteObject(Id.ToString());
        }

        public void EditItem(int Id)
        {
            using (var context = new ScrapeCityDbContext())
            {
                _index.SaveObject(JObject.Parse(JsonConvert.SerializeObject(context.Monitors.Find(Id), Formatting.Indented, converters: new Newtonsoft.Json.Converters.StringEnumConverter())));
            }
        }

        public void UpdateMonitorIndex()
        {
            _index.ClearIndex();

            using (var context = new ScrapeCityDbContext())
            {
                var monitors = context.Monitors.ToList();
                foreach (var monitor in monitors)
                {
                    _index.AddObject(JObject.Parse(JsonConvert.SerializeObject(monitor, Formatting.Indented, converters: new Newtonsoft.Json.Converters.StringEnumConverter())));
                }
            }
        }
    }
}
