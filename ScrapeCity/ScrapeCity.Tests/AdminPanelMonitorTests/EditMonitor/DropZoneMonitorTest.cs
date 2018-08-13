using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Data;
using ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor.Helper;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor
{
    class DropZoneMonitorTest
    {
        IWebDriver driver;
        ScrapeCityDbContext context;
        EditMonitorHelper helper;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 3);
            context = new ScrapeCityDbContext();
            helper = new EditMonitorHelper();
        }

        [Test]
        public void DropZoneMonitor()
        {
            var testImageHeight = 150;
            var testImageWidth = 150;
            var testImage = new Bitmap(testImageWidth, testImageHeight);
            var testImageName = "testImage.png";
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
            Directory.CreateDirectory(physicalPath);

            context.Monitors.Add(monitor);
            context.SaveChanges();
            context.Entry(monitor).State = EntityState.Detached;
            testImage.Save(physicalPath + "\\" + testImageName);

            //get id of newly created monitor
            var monitorToBeEditedId = context.Monitors.FirstOrDefault(x => x.Model == TestSettings.monitorModel).Id;

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //go to edit
            helper.GoToEditPage(driver, TestSettings.brandName, monitorToBeEditedId);

            var dropzone = driver.FindElement(By.Id("dropzoneJsForm"));

            DropFile(dropzone, physicalPath + "\\" + testImageName);
            var uploadButton = driver.FindElement(By.Id("upload-images"));
            uploadButton.Click();


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

        void DropFile(IWebElement target, string filePath, int offsetX = 0, int offsetY = 0)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            IWebDriver driver = ((RemoteWebElement)target).WrappedDriver;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            string JS_DROP_FILE = @"
        var target = arguments[0],
            offsetX = arguments[1],
            offsetY = arguments[2],
            document = target.ownerDocument || document,
            window = document.defaultView || window;

        var input = document.createElement('INPUT');
        input.type = 'file';
        
        input.onchange = function () {
          target.scrollIntoView(true);

          var rect = target.getBoundingClientRect(),
              x = rect.left + (offsetX || (rect.width >> 1)),
              y = rect.top + (offsetY || (rect.height >> 1)),
              dataTransfer = { files: this.files };

          ['dragenter', 'dragover', 'drop'].forEach(function (name) {
            var evt = document.createEvent('MouseEvent');
            evt.initMouseEvent(name, !0, !0, window, 0, 0, 0, x, y, !1, !1, !1, !1, 0, null);
            evt.dataTransfer = dataTransfer;
            target.dispatchEvent(evt);
          });

          setTimeout(function () { document.body.removeChild(input); }, 25);
        };
        document.body.appendChild(input);
        return input;
        ";

            IWebElement input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
            input.SendKeys(filePath);
            wait.Until(ExpectedConditions.StalenessOf(input));
        }
    }
}
