using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ScrapeCity.Data;
using ScrapeCity.Common.Models.Brands;

namespace ScrapeCity.Tests.AdminPanelBrandTests.DeleteBrand
{
    class DeleteBrandTest
    {
        IWebDriver driver;
        ScrapeCityDbContext context;
        TestHelper helper;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            context = new ScrapeCityDbContext();
            helper = new TestHelper();
        }

        [Test]
        public void DeleteBrand()
        {
            //create a brand to delete
            var testBrand = new Brand()
            {
                BrandName = TestSettings.brandName
            };
            context.Brands.Add(testBrand);
            context.SaveChanges();

            //get id of created brand
            var testBrandId = context.Brands.FirstOrDefault(x => x.BrandName == TestSettings.brandName).Id;

            //site url/brand
            driver.Url = TestSettings.AdminPanelUrl + "/brand";

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //find the delete button
            var deleteBrandButtons = driver.FindElements(By.Name("deleteBrandButton"));
            var deleteBrandButton = deleteBrandButtons.FirstOrDefault(x => x.GetAttribute("href").Contains($"/Delete/{testBrandId}"));
            deleteBrandButton.Click();
            //find and click confirmation ok button
            var confirmButtons = driver.FindElements(By.CssSelector(".btn-group > a"));
            var confirmButton = confirmButtons.FirstOrDefault(x => x.GetProperty("href").Contains($"/Brand/Delete/{testBrandId}"));
            confirmButton.Click();
            //run some tests
            var brandCount = context.Brands.Count(x => x.BrandName == TestSettings.brandName);
            Assert.IsTrue(brandCount == 0);
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);

            driver.Close();

            foreach (var brand in context.Brands.Where(x=>x.BrandName == TestSettings.brandName))
            {
                context.Brands.Remove(brand);
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}
