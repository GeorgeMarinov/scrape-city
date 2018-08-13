using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common
{
    public static class Settings
    {
        public static string ImagesDomain => "//" + ConfigurationManager.AppSettings["imgDomain"];
        public static string ImagesServerUploadPath => ConfigurationManager.AppSettings["imgServerUploadPath"];
        public static string AlgoliaMonitorsIndex => ConfigurationManager.AppSettings["Algolia.MonitorsIndex"];
        public static string AlgoliaAppId => ConfigurationManager.AppSettings["Algolia.AppId"];
        public static string AlgoliaAdminApiKey => ConfigurationManager.AppSettings["Algolia.AdminApiKey"];
        public static string AlgoliaSearchApiKey => ConfigurationManager.AppSettings["Algolia.SearchApiKey"];
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ScrapeCityDbContext"].ConnectionString;
        public static string HangfireConnectionString => ConfigurationManager.ConnectionStrings["ScrapeCityHangfire"].ConnectionString;
        public static string DisplaySpecScrapingHDD_Path => ConfigurationManager.AppSettings["DisplaySpecScrapingHDD_Path"];
        public static string imagesHDD_Path;
    }
}
