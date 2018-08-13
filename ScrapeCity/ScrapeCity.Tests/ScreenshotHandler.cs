using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System.IO; 

namespace ScrapeCity.Tests
{
    public static class ScreenshotHandler
    {
        public static void MakeScreenshot(TestContext ctx, IWebDriver driver)
        {
            if (ctx.Result.Outcome == ResultState.Error || ctx.Result.Outcome == ResultState.Failure)
            {
                var screenShotService = driver as ITakesScreenshot;
                var screenshot = screenShotService.GetScreenshot();
               
                var savePath = Path.Combine(TestSettings.ScreenshotsPath, ctx.Test.ClassName, ctx.Test.MethodName);

                Directory.CreateDirectory(savePath);
                var saveFileName = Path.Combine(savePath, "error.png");
                screenshot.SaveAsFile(saveFileName); 
                TestContext.AddTestAttachment(saveFileName);
            }
        }
    }
}
