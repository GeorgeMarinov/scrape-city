using System;
using System.IO;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor.Helper
{
    public class EditMonitorHelper : MonitorHelper
    {
        public string GetPhysicalImageFolderLocation(string monitorBrand, string monitorModel)
        {
            var physicalPath = "";
            if (Path.IsPathRooted(TestSettings.ImagesServerUploadPath))
            {
                physicalPath = Path.Combine(TestSettings.ImagesServerUploadPath, monitorBrand, monitorModel);
            }
            else
            {
                physicalPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDi‌​rectory).Parent.Parent.FullName, TestSettings.ImagesServerUploadPath, monitorBrand, monitorModel);
            }
            return physicalPath;
        }
    }
}
