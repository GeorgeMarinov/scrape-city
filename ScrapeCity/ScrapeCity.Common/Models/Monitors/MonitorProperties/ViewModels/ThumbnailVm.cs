namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class ThumbnailVm
    {
        private string _thumbnail;

        public string Thumbnail
        {
            get
            {
                return $"{Settings.ImagesDomain}/monitor/{_thumbnail}";
            }
            set
            {
                _thumbnail = value;
            }
        }
    }
}