using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.ImageAddresses;
using ScrapeCity.Data;
using ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor.Helper;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor
{
    class DeleteMonitorImageTest
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
        public void DeleteMonitorImage()
        {
            var testImageHeight = 150;
            var testImageWidth = 150;
            var testImageName = "testImage.png";
            var testImage = new Bitmap(testImageWidth, testImageHeight);

            //prepare target to edit
            var testBrand = new Brand() { BrandName = TestSettings.brandName };
            context.Brands.Add(testBrand);

            var monitor = new Common.Models.Monitors.Monitor()
            {
                Model = TestSettings.monitorModel,
            };
            monitor.Brand = testBrand;

            monitor.MonitorImages.Add(new MonitorImageAddress()
            {
                Uri = testImageName
            });

            //construct physical path to images folder
            string physicalPath = helper.GetPhysicalImageFolderLocation(TestSettings.brandName, TestSettings.monitorModel);
            Directory.CreateDirectory(physicalPath);

            context.Monitors.Add(monitor);
            context.SaveChanges();
            context.Entry(monitor).State = EntityState.Detached;
            testImage.Save(physicalPath + "\\" + testImageName);

            //get id of newly created monitor
            var monitorToBeEditedId = context.Monitors.FirstOrDefault(x => x.Model == TestSettings.monitorModel).Id;

            //site url
            driver.Url = TestSettings.AdminPanelUrl;

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //go to edit
            helper.GoToEditPage(driver, TestSettings.brandName, monitorToBeEditedId);

            //find and change thumbnail
            var monitorPictures = driver.FindElements(By.CssSelector(".jumbotron > article > .img-thumbnail"));
            var addedImage = monitorPictures[0];
            addedImage.Click();

            //find and click the change thumbnail button
            var deleteImageButton = driver.FindElement(By.Id("deleteImage"));
            deleteImageButton.Click();

            Thread.Sleep(3000);
            var result = context.Monitors.Find(monitorToBeEditedId);
            context.Entry(result).Reload();
            Assert.Zero(result.MonitorImages.Count);
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
