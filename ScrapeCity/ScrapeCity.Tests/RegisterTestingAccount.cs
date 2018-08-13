using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using ScrapeCity.Data;
using ScrapeCity.Common.Enums;

namespace ScrapeCity.Tests
{
    [Order(1)]
    public class RegisterTestingAccount
    {
        IWebDriver driver;
        ScrapeCityDbContext context;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            context = new ScrapeCityDbContext();
        }

        [Test]
        public void RegisterAccount()
        {
            //check if account exists
            if (context.Users.FirstOrDefault(x=>x.Email == TestSettings.testAccountEmail) == null)
            {
                //create an account
                //navigate to register page
                driver.Url = TestSettings.AdminPanelUrl;
                var registerButton = driver.FindElement(By.Id("registerButton"));
                registerButton.Click();
                //enter account details
                var emailField = driver.FindElement(By.Id("Email"));
                emailField.SendKeys(TestSettings.testAccountEmail);
                var displayNameField = driver.FindElement(By.Id("UserName"));
                displayNameField.SendKeys(TestSettings.testAccountDisplayName);
                var passwordField = driver.FindElement(By.Id("Password"));
                passwordField.SendKeys(TestSettings.testAccountPassword);
                var confirmPasswordField = driver.FindElement(By.Id("ConfirmPassword"));
                confirmPasswordField.SendKeys(TestSettings.testAccountPassword);
                var submitButton = driver.FindElement(By.Id("registerButton"));
                submitButton.Click();
            }

            Assert.NotNull(context.Users.FirstOrDefault(x => x.Email == TestSettings.testAccountEmail));
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);
            driver.Close();
            context.Dispose();
        }
    }
}
