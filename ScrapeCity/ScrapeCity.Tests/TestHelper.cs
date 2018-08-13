using OpenQA.Selenium;
using System.IO;
using System.Security.Permissions;

namespace ScrapeCity.Tests
{
    public class TestHelper
    {
        public void DeleteDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            string[] allPaths = Directory.GetDirectories(directoryPath);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string path in allPaths)
            {
                DeleteDirectory(path);
            }
            var permission = new FileIOPermission(PermissionState.Unrestricted);
            permission.AddPathList(FileIOPermissionAccess.AllAccess, directoryPath);
            Directory.Delete(directoryPath, true);
        }

        public void EnterAccountDetails(IWebDriver driver)
        {
            var emailField = driver.FindElement(By.Id("Email"));
            if (emailField != null)
            {
                emailField.Clear();
                emailField.SendKeys(TestSettings.testAccountEmail);
                var passwordField = driver.FindElement(By.Id("Password"));
                passwordField.Clear();
                passwordField.SendKeys(TestSettings.testAccountPassword);
                var loginButton = driver.FindElement(By.Id("loginButton"));
                loginButton.Click();
            }
        }
    }
}
