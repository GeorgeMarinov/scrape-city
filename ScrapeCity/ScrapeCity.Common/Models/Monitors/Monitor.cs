using Newtonsoft.Json;
using ScrapeCity.Common.Enums;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.ImageAddresses;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapeCity.Common.Models.Monitors
{
    public class Monitor
    {
        public Monitor()
        {
            VideoPorts = new List<VideoPort>();
            USBPorts = new List<USBPort>();
            MonitorImages = new List<MonitorImageAddress>();
            PanelColors = new List<PanelColor>();
            Features = new List<Feature>();
            CertificatesStandartsLicenses = new List<CertificateStandartLicense>();
            Speakers = new Speakers();
            Camera = new Camera();
            UnhandledDisplaySpecificationProperties = new List<UnhandledDIsplaySpecificationProperty>();
        }

        #region Metadata
        /// <summary>
        /// id is used by database
        /// </summary>
        /// 
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }
        /// <summary>
        /// id used by dispayspecifications
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DisplaySpecId { get; set; }
        /// <summary>
        /// id used by algolia
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [NotMapped]
        public string objecId
        {
            get
            {
                return this.Id.ToString();
            }
        }
        #endregion

        #region Images
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Thumbnail { get; set; }
        [JsonIgnore]
        public virtual ICollection<MonitorImageAddress> MonitorImages { get; set; }
        #endregion

        #region Display
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual Brand Brand { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Model { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ScreenSize { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ScreenDiagonal { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ScreenWidth { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ScreenHeight { get; set; }
        public bool HasCurvedPanel { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double RadiusOfCurvature { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PanelType PanelType { get; set; } = PanelType.NotAvailable;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PanelBitDepth { get; set; }
        /// <summary>
        /// frame rate control
        /// </summary>
        public bool FRC { get; set; }
        /// <summary>
        /// look up table
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int LUT { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ColorDepth { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double AspectRatio { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AspectRatioCommon { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxHorizontalPixels { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxVerticalPixels { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PixelPitch { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PPI { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double DisplayArea { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Backlight BacklightType { get; set; } = Backlight.NotAvailable;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double NTSC { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double sRGB { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double AdobeRGB { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double DCI_P3 { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Rec2020 { get; set; }
        /// <summary>
        /// brightness per cd/m2 OR nits
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Brightness { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PeakBrightness { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StaticContrast { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long DynamicContrast { get; set; }
        /// <summary>
        /// expands contrast ratio
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string HDR { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int HorizontalViewingAngle { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int VerticalViewingAngle { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MinimumResponceTime { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AverageResponceTime { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaximumResponceTime { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InputLag { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ScreenType ScreenType { get; set; } = ScreenType.NotAvailable;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double CIE1976 { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double CIE1931 { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double REC709 { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DisplaySyncType DisplaySyncType { get; set; }
        public bool ThreeD { get; set; }
        #endregion

        #region Frequencies
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MinHorizontalFrequency { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxHorizontalFrequency { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int VerticalFrequency { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Minimum110V { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Maximum110V { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Minimum220V { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Maximum220V { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AlternatingCurrentFrequencyMin { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AlternatingCurrentFrequencyMax { get; set; }
        #endregion

        #region Power Consumption
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PowerConsumptionOff { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PowerConsumptionSleep { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PowerConsumptionAverage { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PowerConsumptionEco { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double PowerConsumptionMaximum { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Amperage { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string EnergyEfficiencyClass { get; set; }
        #endregion

        #region Dimensions, weight and color
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Width { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Height { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Depth { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Weight { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double WidthWithStand { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double HeightWithStand { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double DepthWithStand { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double WeightWithStand { get; set; }
        public virtual ICollection<PanelColor> PanelColors { get; set; }
        #endregion

        #region Ergonomics
        public bool VESA_Mount { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int VESA_Interface { get; set; }
        public bool RemoveableStand { get; set; }
        public bool HeightAdjustable { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double HeightAdjustmentRange { get; set; }
        public bool LandscapePortraitPivot { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double LeftPivot { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double RightPivot { get; set; }
        public bool LeftRightSwivel { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double LeftSwivel { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double RightSwivel { get; set; }
        public bool ForwardBackwardTilt { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ForwardTilt { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double BackwardTilt { get; set; }
        #endregion

        public virtual Speakers Speakers { get; set; }

        public virtual Camera Camera { get; set; }

        #region Connectivity
        public virtual ICollection<VideoPort> VideoPorts { get; set; }
        public virtual ICollection<USBPort> USBPorts { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AudioPortIn { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AudioPortOut { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MicrophonePort { get; set; }
        public bool SD_MemoryCardSlot { get; set; }
        public bool EthernetRJ45 { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Network { get; set; }
        #endregion

        #region Features
        public virtual ICollection<Feature> Features { get; set; }
        #endregion

        #region CertificatesStandartsLicenses
        public virtual ICollection<CertificateStandartLicense> CertificatesStandartsLicenses { get; set; }
        #endregion

        #region Unhandled
        [JsonIgnore]
        public virtual ICollection<UnhandledDIsplaySpecificationProperty> UnhandledDisplaySpecificationProperties { get; set; }
        #endregion
    }
}
