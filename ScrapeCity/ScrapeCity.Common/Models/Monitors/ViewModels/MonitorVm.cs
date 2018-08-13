using Newtonsoft.Json;
using ScrapeCity.Common.Enums;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.ImageAddresses;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScrapeCity.Common.Models.Monitors.ViewModels
{
    public class MonitorVm : ThumbnailVm
    {
        public MonitorVm()
        {
            VideoPorts = new List<VideoPortVm>();
            USBPorts = new List<USBPortVm>();
            ViewData = new MonitorViewData();
            MonitorPictures = new List<MonitorPicturesVm>();
            PanelColors = new List<string>();
            Features = new List<string>();
            CertificatesStandartsLicenses = new List<string>();
            Speakers = new SpeakersVm();
            Camera = new CameraVm();
            UnhandledDisplaySpecificationProperties = new List<string>();
        }

        #region Metadata
        /// <summary>
        /// id is used by database
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id used by dispayspecifications
        /// </summary>
        public string DisplaySpecId { get; set; }
        public MonitorViewData ViewData { get; set; }
        #endregion

        #region Images
        //Thumbnail is being inherited
        [Display(Name = "Monitor images")]
        public List<MonitorPicturesVm> MonitorPictures { get; set; }
        #endregion

        #region Display
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; }
        [Display(Name = "Screen size")]
        public double ScreenSize { get; set; }
        [Display(Name = "Screen diagonal")]
        public double ScreenDiagonal { get; set; }
        [Display(Name = "Screen width")]
        public double ScreenWidth { get; set; }
        [Display(Name = "Screen height")]
        public double ScreenHeight { get; set; }
        [Display(Name = "Curved Panel")]
        public bool HasCurvedPanel { get; set; }
        [Display(Name = "Radius of curvature")]
        public double RadiusOfCurvature { get; set; }
        [Display(Name = "Panel type")]
        public string PanelType { get; set; }
        [Display(Name = "Panel bit depth")]
        public int PanelBitDepth { get; set; }
        /// <summary>
        /// frame rate control
        /// </summary>
        public bool FRC { get; set; }
        /// <summary>
        /// look up table
        /// </summary>
        public int LUT { get; set; }
        [Display(Name = "Color depth")]
        public int ColorDepth { get; set; }
        [Display(Name = "Aspect ratio")]
        public double AspectRatio { get; set; }
        [Display(Name = "Aspect ratio")]
        public string AspectRatioCommon { get; set; }
        [Display(Name = "Horizontal resolution")]
        public int MaxHorizontalPixels { get; set; }
        [Display(Name = "Vertical resolution")]
        public int MaxVerticalPixels { get; set; }
        [Display(Name = "Pixel pitch")]
        public double PixelPitch { get; set; }
        [Display(Name = "Pixel per inch")]
        public int PPI { get; set; }
        [Display(Name = "Display area")]
        public double DisplayArea { get; set; }
        [Display(Name = "Backlight")]
        public string BacklightType { get; set; }
        public double NTSC { get; set; }
        public double sRGB { get; set; }
        public double AdobeRGB { get; set; }
        [Display(Name = "DCI P3")]
        public double DCI_P3 { get; set; }
        [Display(Name = "Rec. 2020")]
        public double Rec2020 { get; set; }
        /// <summary>
        /// brightness per cd/m2 OR nits
        /// </summary>
        public int Brightness { get; set; }
        [Display(Name = "Peak brightness")]
        public int PeakBrightness { get; set; }
        [Display(Name = "Static contrast")]
        public int StaticContrast { get; set; }
        [Display(Name = "Dynamic contrast")]
        public long DynamicContrast { get; set; }
        /// <summary>
        /// expands contrast ratio
        /// </summary>
        public string HDR { get; set; }
        [Display(Name = "Horizontal viewing angle")]
        public int HorizontalViewingAngle { get; set; }
        [Display(Name = "Vertical viewing angle")]
        public int VerticalViewingAngle { get; set; }
        [Display(Name = "Minimum response time")]
        public int MinimumResponceTime { get; set; }
        [Display(Name = "Avarage response time")]
        public int AverageResponceTime { get; set; }
        [Display(Name = "Maximum response time")]
        public int MaximumResponceTime { get; set; }
        [Display(Name = "Input lag")]
        public int InputLag { get; set; }
        [Display(Name = "Screen type")]
        public string ScreenType { get; set; }
        [Display(Name = "CIE - 1976")]
        public double CIE1976 { get; set; }
        [Display(Name = "CIE - 1931")]
        public double CIE1931 { get; set; }
        [Display(Name = "REC - 709")]
        public double REC709 { get; set; }
        [Display(Name = "Display sync type")]
        public string DisplaySyncType { get; set; }
        [Display(Name = "3D")]
        public bool ThreeD { get; set; }
        #endregion   

        #region Frequencies
        [Display(Name = "Horizontal refresh rate")]
        public int MinHorizontalFrequency { get; set; }
        [Display(Name = "Horizontal refresh rate")]
        public int MaxHorizontalFrequency { get; set; }
        [Display(Name = "Vertical refresh rate")]
        public int VerticalFrequency { get; set; }
        [Display(Name = "110V")]
        public int Minimum110V { get; set; }
        [Display(Name = "110V")]
        public int Maximum110V { get; set; }
        [Display(Name = "220V")]
        public int Minimum220V { get; set; }
        [Display(Name = "220V")]
        public int Maximum220V { get; set; }
        [Display(Name = "Alternating current frequency")]
        public int AlternatingCurrentFrequencyMin { get; set; }
        [Display(Name = "Alternating current frequency")]
        public int AlternatingCurrentFrequencyMax { get; set; }
        #endregion

        #region Power Consumption
        [Display(Name = "Power consumption (off)")]
        public double PowerConsumptionOff { get; set; }
        [Display(Name = "Power consumption (sleep)")]
        public double PowerConsumptionSleep { get; set; }
        [Display(Name = "Power consumption (avarage)")]
        public double PowerConsumptionAverage { get; set; }
        [Display(Name = "Power consumption (eco)")]
        public double PowerConsumptionEco { get; set; }
        [Display(Name = "Power consumption (maximum)")]
        public double PowerConsumptionMaximum { get; set; }
        public double Amperage { get; set; }
        [Display(Name = "Energy efficiency class")]
        public string EnergyEfficiencyClass { get; set; }
        #endregion

        #region Dimensions, weight and color
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        [Display(Name = "Width with stand")]
        public double WidthWithStand { get; set; }
        [Display(Name = "Height with stand")]
        public double HeightWithStand { get; set; }
        [Display(Name = "Depth with stand")]
        public double DepthWithStand { get; set; }
        [Display(Name = "Weight with stand")]
        public double WeightWithStand { get; set; }
        [Display(Name = "Panel colors")]
        public List<string> PanelColors { get; set; }
        #endregion

        #region Ergonomics
        [Display(Name = "VESA mount")]
        public bool VESA_Mount { get; set; }
        [Display(Name = "VESA interface")]
        public int VESA_Interface { get; set; }
        [Display(Name = "Removable stand")]
        public bool RemoveableStand { get; set; }
        [Display(Name = "Height adjustable")]
        public bool HeightAdjustable { get; set; }
        [Display(Name = "Height adjustment range")]
        public double HeightAdjustmentRange { get; set; }
        [Display(Name = "Landscape portrait pivot")]
        public bool LandscapePortraitPivot { get; set; }
        [Display(Name = "Left pivot")]
        public double LeftPivot { get; set; }
        [Display(Name = "Right pivot")]
        public double RightPivot { get; set; }
        [Display(Name = "Left right swivel")]
        public bool LeftRightSwivel { get; set; }
        [Display(Name = "Left swivel")]
        public double LeftSwivel { get; set; }
        [Display(Name = "Right swivel")]
        public double RightSwivel { get; set; }
        [Display(Name = "Forward backward tilt")]
        public bool ForwardBackwardTilt { get; set; }
        [Display(Name = "Forward tilt")]
        public double ForwardTilt { get; set; }
        [Display(Name = "Backward tilt")]
        public double BackwardTilt { get; set; }
        #endregion
        
        public SpeakersVm Speakers { get; set; }

        public CameraVm Camera { get; set; }

        #region Connectivity
        [Display(Name = "Video ports")]
        public List<VideoPortVm> VideoPorts { get; set; }
        [Display(Name = "USB ports")]
        public List<USBPortVm> USBPorts { get; set; }
        [Display(Name = "Audio port (in)")]
        public string AudioPortIn { get; set; }
        [Display(Name = "Audio port (out)")]
        public string AudioPortOut { get; set; }
        [Display(Name = "Microphone port")]
        public string MicrophonePort { get; set; }
        [Display(Name = "SD Memory card slot")]
        public bool SD_MemoryCardSlot { get; set; }
        [Display(Name = "Ethernet RJ45")]
        public bool EthernetRJ45 { get; set; }
        public string Network { get; set; }
        #endregion

        #region Features
        public List<string> Features { get; set; }
        #endregion

        #region CertificatesStandartsLicenses
        public List<string> CertificatesStandartsLicenses { get; set; }
        #endregion

        #region Unhandled
        public List<string> UnhandledDisplaySpecificationProperties { get; set; }
        #endregion
    }
}