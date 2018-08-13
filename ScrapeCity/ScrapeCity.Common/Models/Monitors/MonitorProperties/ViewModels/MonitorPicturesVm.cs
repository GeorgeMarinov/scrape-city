namespace ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels
{
    public class MonitorPicturesVm
    {
        public int Id { get; set; }
        private string _uri;

        public string Uri {
            get {
                return $"{Settings.ImagesDomain}/monitor/{_uri}";
            }
            set {
                _uri = value;
            }
        }
    }
}
