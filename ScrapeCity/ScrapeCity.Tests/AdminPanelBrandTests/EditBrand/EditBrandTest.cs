using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ScrapeCity.Data;
using ScrapeCity.Common.Models.Brands;

namespace ScrapeCity.Tests.AdminPanelBrandTests.EditBrand
{
    class EditBrandTest
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
        public void EditBrand()
        {
            //create a brand to edit
            var testBrand = new Brand()
            {
                BrandName = TestSettings.brandName
            };
            context.Brands.Add(testBrand);
            context.SaveChanges();

            //get id of created brand
            var testBrandId = context.Brands.FirstOrDefault(x=>x.BrandName == TestSettings.brandName).Id;

            //site url/brand
            driver.Url = TestSettings.AdminPanelUrl + "/brand";

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //find the edit button
            var editBrandButtons = driver.FindElements(By.Name("editBrandButton"));
            var editBrandButton = editBrandButtons.FirstOrDefault(x=>x.GetProperty("href").Contains($"/Edit/{testBrandId}"));
            editBrandButton.Click();
            //find the brand name input and enter a name
            var brandNameField = driver.FindElement(By.Id("BrandName"));
            brandNameField.Clear();
            brandNameField.SendKeys(TestSettings.editedBrandName);
            //find and click submit
            editBrandButton = driver.FindElement(By.Id("editBrandButton"));
            editBrandButton.Click();
            
            Assert.NotNull(context.Brands.FirstOrDefault(x => x.BrandName == TestSettings.editedBrandName));
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);

            driver.Close();

            foreach (var brand in context.Brands.Where(x => x.BrandName == TestSettings.editedBrandName || x.BrandName == TestSettings.brandName))
            {
                context.Brands.Remove(brand);
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}
