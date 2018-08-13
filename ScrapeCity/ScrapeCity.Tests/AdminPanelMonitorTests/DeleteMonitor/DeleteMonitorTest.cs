using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using ScrapeCity.Data;
using System.Linq;
using ScrapeCity.Tests.AdminPanelMonitorTests.DeleteMonitor.Helper;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Monitors;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.DeleteMonitor
{
    class DeleteMonitorTest
    {
        IWebDriver driver;
        ScrapeCityDbContext context;
        DeleteMonitorHelper helper;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            context = new ScrapeCityDbContext();
            helper = new DeleteMonitorHelper();
        }

        [Test]
        public void DeleteMonitor()
        {
            //prepare target to delete
            var testBrand = new Brand() { BrandName = TestSettings.brandName };
            context.Brands.Add(testBrand);

            var monitorToBeDeleted = new Monitor()
            {
                Model = TestSettings.monitorModel,
            };
            monitorToBeDeleted.Brand = testBrand;

            context.Monitors.Add(monitorToBeDeleted);
            context.SaveChanges();

            //get id
            var monitorToBeDeletedId = context.Monitors.FirstOrDefault(x => x.Model == TestSettings.monitorModel).Id;

            driver.Url = TestSettings.AdminPanelUrl;

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            helper.GoToEditPage(driver, TestSettings.brandName, monitorToBeDeletedId);

            var deleteButton = driver.FindElement(By.Id("deleteMonitorButton"));
            deleteButton.Click();
            var confirmButtons = driver.FindElements(By.CssSelector(".btn-group > a"));
            var confirmButton = confirmButtons.FirstOrDefault(x => x.GetProperty("href").Contains($"/Monitor/Delete/{monitorToBeDeletedId}"));
            confirmButton.Click();

            var monitorCount = context.Monitors.Count(x => x.Id == monitorToBeDeletedId);
            Assert.IsTrue(monitorCount == 0);

            //delete the test brand the hard way (since sql is being a fucking bitch)
            driver.Url = TestSettings.AdminPanelUrl + "/brand";

            var deleteBrandButtons = driver.FindElements(By.Name("deleteBrandButton"));
            var deleteBrandButton = deleteBrandButtons.FirstOrDefault(x => x.GetProperty("href").Contains($"/Delete/{testBrand.Id}"));
            deleteBrandButton.Click();
            var deleteBrandConfirmButtons = driver.FindElements(By.CssSelector(".btn-group > a"));
            deleteBrandConfirmButtons.FirstOrDefault(x => x.GetProperty("href").Contains($"/Brand/Delete/{testBrand.Id}")).Click();

            var brandCount = context.Brands.Count(x => x.BrandName == TestSettings.brandName);
            Assert.IsTrue(brandCount == 0);
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
        }
    }
}