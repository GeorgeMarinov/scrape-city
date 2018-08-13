using System.Linq;
using OpenQA.Selenium;

namespace ScrapeCity.Tests.AdminPanelMonitorTests
{
    public class MonitorHelper : TestHelper
    {
        public void GoToEditPage(IWebDriver driver, string brand, int monitorId)
        {
            var indexNavLinks = driver.FindElements(By.CssSelector(".nav-link"));
            var testBrandNavLink = indexNavLinks.FirstOrDefault(x => x.GetProperty("href").Contains(brand));
            testBrandNavLink.Click();
            var links = driver.FindElements(By.CssSelector(".tab-pane > .row > .card > .card-footer > a"));
            var editButton = links.FirstOrDefault(x => x.GetProperty("href").Contains($"/Monitor/Edit/{monitorId}"));
            editButton.Click();
        }
    }
}
