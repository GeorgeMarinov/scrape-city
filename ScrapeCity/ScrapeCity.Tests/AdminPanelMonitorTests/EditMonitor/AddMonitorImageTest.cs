using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Data;
using ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor.Helper;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor
{
    class AddMonitorImageTest
    {
        IWebDriver driver;
        ScrapeCityDbContext context;
        EditMonitorHelper helper;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            context = new ScrapeCityDbContext();
            helper = new EditMonitorHelper();
        }

        [Test]
        public void AddMonitorImage()
        {
            //site url
            driver.Url = TestSettings.AdminPanelUrl;

            //prepare target to edit
            var testBrand = new Brand() { BrandName = TestSettings.brandName };
            context.Brands.Add(testBrand);

            var monitor = new Common.Models.Monitors.Monitor()
            {
                Model = TestSettings.monitorModel,
            };
            monitor.Brand = testBrand;

            //construct physical path to images folder
            string physicalPath = helper.GetPhysicalImageFolderLocation(TestSettings.brandName, TestSettings.monitorModel);

            context.Monitors.Add(monitor);
            context.SaveChanges();
            context.Entry(monitor).State = EntityState.Detached;

            //get id of newly created monitor
            var monitorToBeEditedId = context.Monitors.FirstOrDefault(x => x.Model == TestSettings.monitorModel).Id;

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //go to edit
            helper.GoToEditPage(driver, TestSettings.brandName, monitorToBeEditedId);

            //add images
            //find the add extra upload field button and click it
            var addUploadImageFieldButton = driver.FindElement(By.Id("addUploadImageLink"));
            addUploadImageFieldButton.Click();
            //add image links to all fields
            var uploadImageFields = driver.FindElements(By.Name("uploadImageLink"));
            for (int i = 0; i < uploadImageFields.Count; i++)
            {
                uploadImageFields[i].SendKeys("https://i.imgur.com/P4xrAZB.png");
            }
            //find the reset button and click it
            var resetUploadImageFieldsButton = driver.FindElement(By.Id("resetUploadImageFields"));
            resetUploadImageFieldsButton.Click();
            //add image link to the single field
            var uploadImageField = driver.FindElement(By.Name("uploadImageLink"));
            uploadImageField.SendKeys("https://i.imgur.com/P4xrAZB.png");
            //find and click the upload button
            var uploadImageLinksButton = driver.FindElement(By.Id("uploadImageLinks"));
            uploadImageLinksButton.Click();

            Thread.Sleep(3000);
            var result = context.Monitors.Find(monitorToBeEditedId);
            context.Entry(result).Reload();
            Assert.That(result.MonitorImages.Count > 0);
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);

            driver.Close();

            foreach (var monitor in context.Monitors.Where(x => x.Model == TestSettings.monitorModel))
            {
                context.Monitors.Remove(monitor);
            }
            foreach (var brand in context.Brands.Where(x => x.BrandName == TestSettings.brandName))
            {
                context.Brands.Remove(brand);
            }
            context.SaveChanges();
            context.Dispose();
            helper.DeleteDirectory(Directory.GetParent(helper.GetPhysicalImageFolderLocation(TestSettings.brandName, TestSettings.monitorModel)).FullName);
        }
    }
}
