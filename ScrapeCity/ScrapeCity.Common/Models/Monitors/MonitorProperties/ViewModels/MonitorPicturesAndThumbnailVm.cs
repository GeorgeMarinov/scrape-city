using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class MonitorPicturesAndThumbnailVm
    {
        public MonitorPicturesAndThumbnailVm()
        {
            MonitorPictures = new List<MonitorPicturesVm>();
        }

        public List<MonitorPicturesVm> MonitorPictures { get; set; }

        public string Thumbnail { get; set; }
    }
}
