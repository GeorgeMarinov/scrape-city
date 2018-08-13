using System;
using System.Configuration;

namespace ScrapeCity.Tests
{
    public static class TestSettings
    {
        public static string AdminPanelUrl => ConfigurationManager.AppSettings["AdminPanelUrl"];
        public static string ScreenshotsPath => ConfigurationManager.AppSettings["ScreenshotsPath"];
        public static string testAccountEmail => ConfigurationManager.AppSettings["SeleniumTestEmail"];
        public static string testAccountDisplayName => ConfigurationManager.AppSettings["SeleniumTestUsername"];
        public static string testAccountPassword => ConfigurationManager.AppSettings["SeleniumTestPassword"];
        public static string ImagesServerUploadPath => ConfigurationManager.AppSettings["imgServerUploadPath"];
        public static string brandName => "TestBrand";
        public static string editedBrandName => "EditedTestBrand";
        public static string monitorModel => "TestModel";
        public static string monitorEditedModel => "EditedTestModel";
    }
}
