using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ScrapeCity.Data;

namespace ScrapeCity.Tests.AdminPanelBrandTests.AddBrand
{
    class AddBrandTest
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
        public void AddBrand()
        {
            //site url/brand
            driver.Url = TestSettings.AdminPanelUrl + "/brand";

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //find and click the add brand button
            var addBrandButton = driver.FindElement(By.Id("addBrandButton"));
            addBrandButton.Click();
            //find the brand name input and enter a name
            var brandNameField = driver.FindElement(By.Id("BrandName"));
            brandNameField.SendKeys(TestSettings.brandName);
            //find and click submit
            var createBrandButton = driver.FindElement(By.Id("createBrandButton"));
            createBrandButton.Click();
            
            Assert.NotNull(context.Brands.FirstOrDefault(x => x.BrandName == TestSettings.brandName));
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);

            driver.Close();

            foreach (var brand in context.Brands.Where(x => x.BrandName == TestSettings.brandName))
            {
                context.Brands.Remove(brand);
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}
