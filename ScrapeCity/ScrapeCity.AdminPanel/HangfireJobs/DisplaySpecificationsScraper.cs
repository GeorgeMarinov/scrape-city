using Hangfire;
using HtmlAgilityPack;
using ScrapeCity.Common;
using ScrapeCity.Common.Enums;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using ScrapeCity.Domain;
using ScrapeCity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ScrapeCity.AdminPanel.HangfireJobs
{
    public class DisplaySpecificationsScraper : AbstractService , IDisplaySpecificationsScraper
    {
        private IAlgoliaMonitorIndex index;

        public DisplaySpecificationsScraper(IAlgoliaMonitorIndex index)
        {
            this.index = index;
        }

        private string DisplaySpecificationsIndexPage = "https://www.displayspecifications.com/";

        /// <summary>
        /// needs monitor id prepended
        /// </summary>
        private string DisplaySpecificationsMonitorPage = "https://www.displayspecifications.com/en/model/";

        public string[] BrandsToScrape { get; set; } =
        {
            "Acer",
            "AOC",
            "Asus",
            "BenQ",
            "Dell",
            "HP",
            "Lenovo",
            "LG",
            "Philips",
            "Samsung"
        };

        public void Scrape()
        {
            ParseHtmlFromHDD();

            var brandURLs = GetBrandURLs();

            var allDisplaySpecIds = GetAllMonitorUrls(brandURLs);

            var newDisplaySpecIds = GetNewDisplaySpecIds(allDisplaySpecIds);

            for (var i = 0; i < newDisplaySpecIds.Count; i++)
            {
                BackgroundJob.Schedule(() =>
                    this.ParseDisplaySpecificationHtml(newDisplaySpecIds[i]),
                    TimeSpan.FromMinutes(1 + i)
                );
            }
        }

        private List<string> GetBrandURLs()
        {
            var brandURLs = new List<string>();

            var webParser = new HtmlWeb();

            var indexHtml = webParser.Load(DisplaySpecificationsIndexPage);
            var brandsContainer = indexHtml
                .DocumentNode
                .Descendants("div")
                .Where(x =>
                    x.HasClass("brand-listing-container-frontpage")
                 )
                .FirstOrDefault();

            var allAnchorTags = brandsContainer.ChildNodes.ToArray();

            foreach (var tag in allAnchorTags)
            {
                if (BrandsToScrape.Contains(tag.InnerText))
                {
                    brandURLs.Add(tag.Attributes["href"].Value);
                }
            }

            return brandURLs;
        }

        private List<string> GetAllMonitorUrls(List<string> brandURLs)
        {
            var displaySpecIds = new List<string>();

            var webParser = new HtmlWeb();

            foreach (var brand in brandURLs)
            {
                var brandPage = webParser.Load(brand);

                var mainDiv = brandPage
                    .DocumentNode
                    .Descendants("div")
                    .Where(x =>
                        x.Attributes["id"].Value == "main"
                     )
                    .FirstOrDefault();

                var monitorsPerYear = mainDiv
                    .Descendants()
                    .Where(x =>
                        x.Name.Contains("div")
                        &&
                        x.HasClass("model-listing-container-80")
                        &&
                        x.PreviousSibling.InnerText.Contains("monitors")
                    )
                    .ToArray();

                for (int i = 0; i < monitorsPerYear.Length; i++)
                {
                    var monitorDivs = monitorsPerYear[i].ChildNodes;
                    for (int y = 0; y < monitorDivs.Count; y++)
                    {
                        var monitorId = monitorDivs[y].Id;
                        monitorId = monitorId.Split(new[] { "model_" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        displaySpecIds.Add(monitorId);
                    }
                }
            }

            return displaySpecIds;
        }

        private List<string> GetNewDisplaySpecIds(List<string> displaySpecIds)
        {
            var storedDisplaySpecIds = context.Monitors.Select(x => x.DisplaySpecId).ToList();

            return displaySpecIds.Except(storedDisplaySpecIds).ToList();
        }

        private void ParseHtmlFromHDD()
        {
            var pathsToFiles = Directory.GetFiles(Settings.DisplaySpecScrapingHDD_Path, "*", SearchOption.AllDirectories);

            for (int currentPath = 0; currentPath < pathsToFiles.Length; currentPath++)
            {
                var monitor = new Monitor();
                var file = File.ReadAllText(pathsToFiles[currentPath]);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(file);

                monitor.DisplaySpecId = ParseDisplaySpecId(htmlDoc);

                if (!context.Monitors.Any(x => x.DisplaySpecId == monitor.DisplaySpecId))
                {
                    var tableRows = htmlDoc.DocumentNode.Descendants("tr").ToArray();
                    monitor = ParseMonitor(monitor, tableRows);
                    monitor.Thumbnail = ParseThumbnail(htmlDoc, monitor);

                    context.Monitors.Add(monitor);
                    context.SaveChanges();
                    File.Delete(pathsToFiles[currentPath]);
                }
            }
        }

        public void ParseDisplaySpecificationHtml(string newDisplaySpecId)
        {
            var monitor = new Monitor();

            var web = new HtmlWeb();
            var htmlDoc = web.Load(DisplaySpecificationsMonitorPage + newDisplaySpecId);

            monitor.DisplaySpecId = ParseDisplaySpecId(htmlDoc);

            var tableRows = htmlDoc.DocumentNode.Descendants("tr").ToArray();
            monitor = ParseMonitor(monitor, tableRows);
            monitor.Thumbnail = ParseThumbnail(htmlDoc, monitor);

            context.Monitors.Add(monitor);
            context.SaveChanges();

            index.AddItem(monitor.Id);
        }

        private Monitor ParseMonitor(Monitor monitor, HtmlNode[] tableRows)
        {
            for (int i = 0; i < tableRows.Length - 4; i++)
            {
                var key = "";
                var value = "";
                var keyValuePairs = tableRows[i].ChildNodes.ToArray();
                if (keyValuePairs[0].HasChildNodes)
                {
                    if (!string.IsNullOrEmpty(keyValuePairs[0].FirstChild.InnerText) || !string.IsNullOrWhiteSpace(keyValuePairs[0].FirstChild.InnerText))
                    {
                        key = keyValuePairs[0].FirstChild.InnerText;
                    }
                    if (!string.IsNullOrEmpty(keyValuePairs[1].InnerText) || !string.IsNullOrWhiteSpace(keyValuePairs[1].InnerText))
                    {
                        value = keyValuePairs[1].InnerText;
                    }
                }
                if (key == "Brand")
                {
                    monitor.Brand = ParseBrand(value);
                }
                else if (key == "Model")
                {
                    monitor.Model = ParseModel(value);
                }
                else if (key == "Size class")
                {
                    monitor.ScreenSize = ParseScreenSize(value);
                }
                else if (key == "Diagonal")
                {
                    monitor.ScreenDiagonal = ParseScreenDiagonal(value);
                }
                else if (key == "Width")
                {
                    monitor.ScreenWidth = ParseScreenWidth(value);
                    monitor.Width = ParseWidth(value,keyValuePairs);
                }
                else if (key == "Height")
                {
                    monitor.ScreenHeight = ParseScreenHeight(value);
                    monitor.Height = ParseHeight(value, keyValuePairs);
                }
                else if (key == "Radius of curvature")
                {
                    monitor.RadiusOfCurvature = ParseRadiusOfCurvature(value);
                    monitor.HasCurvedPanel = true;
                }
                else if (key == "Panel type")
                {
                    monitor.PanelType = ParsePanelType(value);
                }
                else if (key == "Panel bit depth")
                {
                    monitor.PanelBitDepth = ParsePanelBitDepth(value);
                }
                else if (key == "FRC")
                {
                    monitor.FRC = ParseFRC(value);
                }
                else if (key == "LUT")
                {
                    monitor.LUT = ParseLUT(value);
                }
                else if (key == "Colors" && value.Contains("bits"))
                {
                    monitor.ColorDepth = ParseColorDepth(value);
                }
                else if (key == "Aspect ratio")
                {
                    monitor.AspectRatio = ParseAspectRatio(value);
                    monitor.AspectRatioCommon = ParseAspectRatioCommon(value);
                }
                else if (key == "Resolution")
                {
                    monitor.MaxHorizontalPixels = ParseHorizontalResolution(value);
                    monitor.MaxVerticalPixels = ParseVerticalResolution(value);
                }
                else if (key == "Pixel pitch")
                {
                    monitor.PixelPitch = ParsePixelPitch(value);
                }
                else if (key == "Pixel density")
                {
                    monitor.PPI = ParsePPI(value);
                }
                else if (key == "Display area")
                {
                    monitor.DisplayArea = ParseDisplayArea(value);
                }
                else if (key == "Backlight")
                {
                    monitor.BacklightType = ParseBacklightType(value);
                }
                else if (key == "NTSC (1953)")
                {
                    monitor.NTSC = ParseNTSC(value);
                }
                else if (key == "sRGB")
                {
                    monitor.sRGB = Parse_sRGB(value);
                }
                else if (key == "Adobe RGB (1998)")
                {
                    monitor.AdobeRGB = ParseAdobeRGB(value);
                }
                else if (key == "DCI P3")
                {
                    monitor.DCI_P3 = ParseDCI_P3(value);
                }
                else if (key == "Rec. 2020")
                {
                    monitor.Rec2020 = ParseRec2020(value);
                }
                else if (key == "Brightness")
                {
                    monitor.Brightness = ParseBrightness(value);
                }
                else if (key == "Peak brightness")
                {
                    monitor.PeakBrightness = ParsePeakBrightness(value);
                }
                else if (key == "Static contrast")
                {
                    monitor.StaticContrast = ParseStaticContrast(value);
                }
                else if (key == "Dynamic contrast")
                {
                    monitor.DynamicContrast = ParseDynamicContrast(value);
                }
                else if (key == "HDR")
                {
                    monitor.HDR = value;
                }
                else if (key == "Horizontal viewing angle")
                {
                    monitor.HorizontalViewingAngle = ParseHorizontalViewingAngle(value);
                }
                else if (key == "Vertical viewing angle")
                {
                    monitor.VerticalViewingAngle = ParseVerticalViewingAngle(value);
                }
                else if (key == "Minimum response time")
                {
                    monitor.MinimumResponceTime = ParseMinimumResponseTime(value);
                }
                else if (key == "Average response time")
                {
                    monitor.AverageResponceTime = ParseAvarageResponseTime(value);
                }
                else if (key == "Maximum response time")
                {
                    monitor.MaximumResponceTime = ParseMaximumResponseTime(value);
                }
                else if (key == "Input lag")
                {
                    monitor.InputLag = ParseInputLag(value);
                }
                else if (key == "Coating")
                {
                    monitor.ScreenType = ParseScreenType(value);
                }
                else if ((key == "" && value.Contains("CIE1976")) || key == "" && value.Contains("CIE1931"))
                {
                    monitor.CIE1976 = ParseCIE1976(value);
                    monitor.CIE1931 = ParseCIE1931(value);
                }
                else if (key == "" && value.Contains(Regex.Match(value, "REC\\W*709").Value))
                {
                    monitor.REC709 = ParseRec709(value);
                }
                else if (key == "3D")
                {
                    monitor.ThreeD = ParseThreeD(value);
                }
                else if (key == "Vertical frequency (digital)")
                {
                    monitor.VerticalFrequency = ParseVerticalFrequency(value);
                }
                else if (key == "Horizontal frequency (digital)")
                {
                    monitor.MinHorizontalFrequency = ParseMinHorizontalFrequency(value);
                    monitor.MaxHorizontalFrequency = ParseMaxHorizontalFrequency(value);
                }
                else if (key == "110V")
                {
                    monitor.Minimum110V = ParseMin110V(value);
                    monitor.Maximum110V = ParseMax110V(value);
                }
                else if (key == "220V")
                {
                    monitor.Minimum220V = ParseMin220V(value);
                    monitor.Maximum220V = ParseMax220V(value);
                }
                else if (key == "Alternating current frequency")
                {
                    monitor.AlternatingCurrentFrequencyMin = ParseAlternatingCurrentFrequencyMin(value);
                    monitor.AlternatingCurrentFrequencyMax = ParseAlternatingCurrentFrequencyMax(value);
                }
                else if (key == "Power consumption (off)")
                {
                    monitor.PowerConsumptionOff = ParsePowerCunsumptionOff(value);
                }
                else if (key == "Power consumption (sleep)")
                {
                    monitor.PowerConsumptionSleep = ParsePowerConsumptionSleep(value);
                }
                else if (key == "Power consumption (average)")
                {
                    monitor.PowerConsumptionAverage = ParsePowerConsumptionAverage(value);
                }
                else if (key == "Power consumption (eco)")
                {
                    monitor.PowerConsumptionEco = ParsePowerConsumptionEco(value);
                }
                else if (key == "Power consumption (maximum)")
                {
                    monitor.PowerConsumptionMaximum = ParsePowerConsumptionMaximum(value);
                }
                else if (key == "Amperage")
                {
                    monitor.Amperage = ParseAmperage(value);
                }
                else if (key == "Energy efficiency class")
                {
                    monitor.EnergyEfficiencyClass = ParseEnergyEfficiencyClass(value);
                }
                else if (key == "Depth")
                {
                    monitor.Depth = ParseDepth(value);
                }
                else if (key == "Weight")
                {
                    monitor.Weight = ParseWeight(value);
                }
                else if (key == "Width with stand")
                {
                    monitor.WidthWithStand = ParseWidthWithStand(value);
                }
                else if (key == "Height with stand")
                {
                    monitor.HeightWithStand = ParseHeightWithStand(value);
                }
                else if (key == "Depth with stand")
                {
                    monitor.DepthWithStand = ParseDepthWithStand(value);
                }
                else if (key == "Weight with stand")
                {
                    monitor.WeightWithStand = ParseWeightWithStand(value);
                }
                else if (key == "Colors" && !value.Contains("bits"))
                {
                    monitor.PanelColors = ParsePanelColors(keyValuePairs[1]);
                }
                else if (key == "VESA mount")
                {
                    monitor.VESA_Mount = ParseVESA_Mount(value);
                }
                else if (key == "VESA interface")
                {
                    monitor.VESA_Interface = ParseVESA_Interface(value);
                }
                else if (key == "Removable stand")
                {
                    monitor.RemoveableStand = ParseRemovableStand(value);
                }
                else if (key == "Height adjustment")
                {
                    monitor.HeightAdjustable = ParseHeightAdjustable(value);
                }
                else if (key == "Height adjustment range")
                {
                    monitor.HeightAdjustmentRange = ParseHeightAdjustmentRange(value);
                }
                else if (key == "Landscape/portrait pivot")
                {
                    monitor.LandscapePortraitPivot = ParseLandscapePortraitPivot(value);
                }
                else if (key == "Left pivot")
                {
                    monitor.LeftPivot = ParseLeftPivot(value);
                }
                else if (key == "Right pivot")
                {
                    monitor.RightPivot = ParseRightPivot(value);
                }
                else if (key == "Left/right swivel")
                {
                    monitor.LeftRightSwivel = ParseLeftRightSwivel(value);
                }
                else if (key == "Left swivel")
                {
                    monitor.LeftSwivel = ParseLeftSwivel(value);
                }
                else if (key == "Right swivel")
                {
                    monitor.RightSwivel = ParseRightSwivel(value);
                }
                else if (key == "Forward/backward tilt")
                {
                    monitor.ForwardBackwardTilt = ParseForwardBackwardTilt(value);
                }
                else if (key == "Forward tilt")
                {
                    monitor.ForwardTilt = ParseForwardTilt(value);
                }
                else if (key == "Backward tilt")
                {
                    monitor.BackwardTilt = ParseBackwardTilt(value);
                }
                else if (key == "Speakers")
                {
                    monitor.Speakers = ParseSpeakers(value);
                }
                else if (key == "Camera")
                {
                    monitor.Camera = ParseHasCamera(value);
                }
                else if (key == "Image resolution")
                {
                    monitor.Camera.ImageResolutionPixels = ParseCameraImageResolutionPixels(value);
                    monitor.Camera.ImageResolutionMegaPixels = ParseCameraImageResolutionMegaPixels(value);
                }
                else if (key == "Video resolution")
                {
                    monitor.Camera.VideoResolutionPixels = ParseCameraVideoResolutionPixels(value);
                    monitor.Camera.VideoResolutionMegaPixels = ParseCameraVideoResolutionMegaPixels(value);
                }
                else if (key == "Network")
                {
                    monitor.Network = ParseNetwork(value);
                }
                else if (key == "Connectivity")
                {
                    monitor.VideoPorts = ParseVideoPorts(keyValuePairs[1]);
                    monitor.USBPorts = ParseUSB_Ports(keyValuePairs[1]);
                    monitor.AudioPortIn = ParseAudioIn(keyValuePairs[1]);
                    monitor.AudioPortOut = ParseAudioOut(keyValuePairs[1]);
                    monitor.MicrophonePort = ParseMicrophonePort(keyValuePairs[1]);
                    monitor.SD_MemoryCardSlot = ParseSD_Card(keyValuePairs[1]);
                    monitor.EthernetRJ45 = ParseEthernet(keyValuePairs[1]);
                }
                else if (key == "Features" || key == "Additional features")
                {
                    monitor.Features = ParseFeatures(keyValuePairs[1]);
                }
                else if (key == "Certificates, standards and licenses")
                {
                    monitor.CertificatesStandartsLicenses = ParseCSL(keyValuePairs[1]);
                }
                else
                {
                    monitor.UnhandledDisplaySpecificationProperties.Add(new UnhandledDIsplaySpecificationProperty() { Value = $"key :{key}, value :{value}" });
                }
            }

            return monitor;
        }

        #region ParsingRules

        private string ParseDisplaySpecId(HtmlDocument htmlDoc)
        {
            return Regex.Match(htmlDoc.DocumentNode.InnerHtml,
                "\"https:\\/\\/www\\.displayspecifications\\.com\\/en\\/model\\/(?<match>[^\"]+)\"")
                .Groups["match"]
                .Value;
        }

        private string ParseThumbnail(HtmlDocument htmlDoc, Monitor monitor)
        {
            var imageLocation = Regex.Match(htmlDoc.DocumentNode.InnerHtml, "url\\((?<match>[^)]+)\\)").Groups["match"].Value;

            if (Uri.IsWellFormedUriString(imageLocation, UriKind.RelativeOrAbsolute))
            {
                var pathToCurrentImageDestination = Path.Combine(Settings.imagesHDD_Path, "Monitor", monitor.Brand.BrandName, monitor.Model);
                var fileName = Guid.NewGuid().ToString() + ".jpg";

                if (DownloadThumbnail(imageLocation, pathToCurrentImageDestination, fileName))
                {
                    return $@"/{monitor.Brand.BrandName}/{monitor.Model}/{fileName}";
                }
            }
            return "";
        }

        private bool DownloadThumbnail(string imageLocation, string pathToCurrentImageDestination, string fileName)
        {
            try
            {
                var wc = new WebClient();
                Directory.CreateDirectory(pathToCurrentImageDestination);
                wc.DownloadFile(imageLocation, Path.Combine(pathToCurrentImageDestination, fileName));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private Brand ParseBrand(string value)
        {
            if (context.Brands.Any(x => x.BrandName == value))
            {
                return context.Brands.FirstOrDefault(x => x.BrandName == value);
            }
            else
            {
                var brand = new Brand();
                brand.BrandName = value;
                context.Brands.Add(brand);
                return brand;
            }
        }

        private string ParseModel(string value)
        {
            return value;
        }

        private double ParseScreenSize(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) in").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseScreenDiagonal(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseScreenWidth(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseScreenHeight(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseRadiusOfCurvature(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private PanelType ParsePanelType(string value)
        {
            var panelType = PanelType.NotAvailable;
            value = value.Replace("+", "_Plus");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            var result = Enum.TryParse(value, true, out panelType);
            if (result == false)
            {
                panelType = PanelType.NotAvailable;
            }
            return panelType;
        }

        private int ParsePanelBitDepth(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) bits").Groups["match"].Value, out int result);
            return result;
        }

        private bool ParseFRC(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }

            return true;
        }

        private int ParseLUT(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) bits").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseColorDepth(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) bits").Groups["match"].Value, out int result);
            return result;
        }

        private double ParseAspectRatio(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+):1").Groups["match"].Value, out double result);
            return result;
        }

        private string ParseAspectRatioCommon(string value)
        {
            return Regex.Match(value, "[0-9.]+:1(?<match>[0-9]+:[0-9]+)").Groups["match"].Value;
        }

        private int ParseHorizontalResolution(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) x ").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseVerticalResolution(string value)
        {
            int.TryParse(Regex.Match(value, " x (?<match>[0-9]+)").Groups["match"].Value, out int result);
            return result;
        }

        private double ParsePixelPitch(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private int ParsePPI(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) ppi").Groups["match"].Value, out int result);
            return result;
        }

        private double ParseDisplayArea(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private Backlight ParseBacklightType(string value)
        {
            if (value == "CCFL direct backlight")
            {
                return Backlight.W_LED_17S4P;
            }

            var backlightType = Backlight.NotAvailable;
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            var result = Enum.TryParse(value, true, out backlightType);

            if (result == false)
            {
                backlightType = Backlight.NotAvailable;
            }

            return backlightType;
        }

        private double ParseNTSC(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private double Parse_sRGB(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseAdobeRGB(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseDCI_P3(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseRec2020(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) %").Groups["match"].Value, out double result);
            return result;
        }

        private int ParseBrightness(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) cd\\/m").Groups["match"].Value, out int result);
            return result;
        }

        private int ParsePeakBrightness(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) cd\\/m").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseStaticContrast(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) : 1").Groups["match"].Value, out int result);
            return result;
        }

        private long ParseDynamicContrast(string value)
        {
            long.TryParse(Regex.Match(value, "(?<match>[0-9]+) : 1").Groups["match"].Value, out long result);
            return result;
        }

        private int ParseHorizontalViewingAngle(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+)").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseVerticalViewingAngle(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+)").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMinimumResponseTime(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) ms").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseAvarageResponseTime(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) ms").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMaximumResponseTime(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) ms").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseInputLag(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) ms").Groups["match"].Value, out int result);
            return result;
        }

        private ScreenType ParseScreenType(string value)
        {
            var screenType = ScreenType.NotAvailable;
            value = value.Replace("/", "_");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace("(", "");
            value = value.Replace(")", "");

            var result = Enum.TryParse(value, true, out screenType);

            if (result == false)
            {
                screenType = ScreenType.NotAvailable;
            }

            return screenType;
        }

        private double ParseCIE1976(string value)
        {
            double.TryParse(Regex.Match(value, @"(CIE1972|CIE1976)\W*(?<match>[0-9.]+)\W*%").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseCIE1931(string value)
        {
            double.TryParse(Regex.Match(value, @"CIE1931\W*(?<match>[0-9.]+)\W*%").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseRec709(string value)
        {
            double.TryParse(Regex.Match(value, @"\W*(?<match>[0-9.]+)\W*%").Groups["match"].Value, out double result);
            return result;
        }

        private bool ParseThreeD(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }
            return true;
        }

        private int ParseVerticalFrequency(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) Hz").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMinHorizontalFrequency(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9.]+) kHz - ").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMaxHorizontalFrequency(string value)
        {
            int.TryParse(Regex.Match(value, @"(?<match>[0-9.]+) kHz \(kilohertz\)").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMin110V(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) V -").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMax110V(string value)
        {
            int.TryParse(Regex.Match(value, "- (?<match>[0-9]+) V").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMin220V(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) V -").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseMax220V(string value)
        {
            int.TryParse(Regex.Match(value, "- (?<match>[0-9]+) V").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseAlternatingCurrentFrequencyMin(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) Hz -").Groups["match"].Value, out int result);
            return result;
        }

        private int ParseAlternatingCurrentFrequencyMax(string value)
        {
            int.TryParse(Regex.Match(value, "- (?<match>[0-9]+) Hz").Groups["match"].Value, out int result);
            return result;
        }

        private double ParsePowerCunsumptionOff(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) W").Groups["match"].Value, out double result);
            return result;
        }

        private double ParsePowerConsumptionSleep(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) W").Groups["match"].Value, out double result);
            return result;
        }

        private double ParsePowerConsumptionAverage(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) W").Groups["match"].Value, out double result);
            return result;
        }

        private double ParsePowerConsumptionEco(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) W").Groups["match"].Value, out double result);
            return result;
        }

        private double ParsePowerConsumptionMaximum(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) W").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseAmperage(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) A").Groups["match"].Value, out double result);
            return result;
        }

        private string ParseEnergyEfficiencyClass(string value)
        {
            return value;
        }

        private double ParseWidth(string value, HtmlNode[] keyValuePairs)
        {
            var result = 0d;

            foreach (var item in keyValuePairs)
            {
                if (item.InnerText.Contains("Width without stand in different measurement units."))
                {
                    double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out result);
                    return result;
                }
            }

            return result;
        }

        private double ParseHeight(string value, HtmlNode[] keyValuePairs)
        {
            var result = 0d;

            foreach (var item in keyValuePairs)
            {
                if (item.InnerText.Contains("Height without stand in different measurement units."))
                {
                    double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out result);
                    return result;
                }
            }

            return result;
        }

        private double ParseDepth(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseWeight(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) kg").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseWidthWithStand(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseHeightWithStand(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseDepthWithStand(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseWeightWithStand(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) kg").Groups["match"].Value, out double result);
            return result;
        }

        private ICollection<PanelColor> ParsePanelColors(HtmlNode htmlNode)
        {
            var result = new List<PanelColor>();
            var childNodes = htmlNode.ChildNodes.ToArray();

            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;

                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (context.PanelColors.Any(x => x.Value == value))
                    {
                        result.Add(context.PanelColors.FirstOrDefault(x => x.Value == value));
                    }
                    else
                    {
                        result.Add(new PanelColor() { Value = value });
                    }
                }
            }
            return result;
        }

        private bool ParseVESA_Mount(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }

            return true;
        }

        private int ParseVESA_Interface(string value)
        {
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) mm").Groups["match"].Value, out int result);
            return result;
        }

        private bool ParseRemovableStand(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }
            return true;
        }

        private bool ParseHeightAdjustable(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }
            return true;
        }

        private double ParseHeightAdjustmentRange(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+) mm").Groups["match"].Value, out double result);
            return result;
        }

        private bool ParseLandscapePortraitPivot(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }

            return true;
        }

        private double ParseLeftPivot(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseRightPivot(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private bool ParseLeftRightSwivel(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }

            return true;
        }

        private double ParseLeftSwivel(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseRightSwivel(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private bool ParseForwardBackwardTilt(string value)
        {
            if (value.ToLower().Contains("no"))
            {
                return false;
            }

            return true;
        }

        private double ParseForwardTilt(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private double ParseBackwardTilt(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9.]+)").Groups["match"].Value, out double result);
            return result;
        }

        private Speakers ParseSpeakers(string value)
        {
            var speakers = new Speakers();
            int.TryParse(Regex.Match(value, "(?<match>[0-9]+) x").Groups["match"].Value, out int quantity);
            speakers.Quantity = quantity;
            int.TryParse(Regex.Match(value, "x (?<match>[0-9]+) W").Groups["match"].Value, out int watts);
            speakers.Watts = watts;

            if (speakers.Quantity > 0 || speakers.Watts > 0)
            {
                speakers.HasSpeakers = true;
            }

            return speakers;
        }

        private Camera ParseHasCamera(string value)
        {
            var camera = new Camera();
            if (value.ToLower().Contains("no"))
            {
                camera.HasCamera = false;
            }
            else
            {
                camera.HasCamera = true;
            }

            return camera;
        }

        private string ParseCameraImageResolutionPixels(string value)
        {
            return Regex.Match(value, "(?<match>[0-9]+\\D*[0-9+]+) pixels").Groups["match"].Value;
        }

        private double ParseCameraImageResolutionMegaPixels(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9\\.]+) MP").Groups["match"].Value, out double result);
            return result;
        }

        private string ParseCameraVideoResolutionPixels(string value)
        {
            return Regex.Match(value, "(?<match>[0-9]+\\D*[0-9+]+) pixels").Groups["match"].Value;
        }

        private double ParseCameraVideoResolutionMegaPixels(string value)
        {
            double.TryParse(Regex.Match(value, "(?<match>[0-9\\.]+) MP").Groups["match"].Value, out double result);
            return result;
        }

        private string ParseNetwork(string value)
        {
            return value;
        }

        private ICollection<VideoPort> ParseVideoPorts(HtmlNode htmlNode)
        {
            var videoPorts = new List<VideoPort>();
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    var videoPort = new VideoPort();
                    int.TryParse(Regex.Match(value, "(?<match>[0-9]+) x").Groups["match"].Value, out int quantity);
                    videoPort.Num = quantity;

                    if (value.ToLower().Contains("usb") || value.ToLower().Contains("audio") || value.ToLower().Contains("ethernet") || value.ToLower().Contains("memory") || value.ToLower().Contains("microphone"))
                    {
                        //ignored
                    }
                    else if (Regex.IsMatch(value, "displayport.*1\\.2.*mini.*", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.DisplayPort_1_2_mini;
                    }
                    else if (Regex.IsMatch(value, "displayport.*1\\.4.*mini.*", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.DisplayPort_1_4_mini;
                    }
                    else if (Regex.IsMatch(value, "av comp", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.AV_component_input;
                    }
                    else if (Regex.IsMatch(value, "rs232", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.RS232_C;
                    }
                    else if (Regex.IsMatch(value, "hdmi.*2\\.0.*mhl.*", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.HDMI_2_0_MHL;
                    }
                    else if (Regex.IsMatch(value, "dvi.*d", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.DVI_D_Dual_Link;
                    }
                    else if (Regex.IsMatch(value, "displayport.*1.2.*out.*", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.DisplayPort_1_2_Output;
                    }
                    else if (Regex.IsMatch(value, "hdmi.*1\\.4.*mhl.*", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.HDMI_1_4_MHL;
                    }
                    else if (Regex.IsMatch(value, "displayport.*out", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.DisplayPort_Output;
                    }
                    else if (Regex.IsMatch(value, "d-sub", RegexOptions.IgnoreCase))
                    {
                        videoPort.Type = VideoPortEnum.D_sub;
                    }
                    else
                    {
                        value = Regex.Match(value, "x (?<match>.+)").Groups["match"].Value;
                        var type = VideoPortEnum.not_available;
                        value = value.Replace(" ", "_");
                        value = value.Replace("-", "_");
                        value = value.Replace(".", "_");
                        value = value.Replace("(", "");
                        value = value.Replace(")", "");
                        if (Enum.TryParse(value, true, out type))
                        {
                            videoPort.Type = type;
                        }
                    }
                    if (videoPort.Type != VideoPortEnum.not_available)
                    {
                        videoPorts.Add(videoPort);
                    }
                }
            }
            return videoPorts;
        }

        private ICollection<USBPort> ParseUSB_Ports(HtmlNode htmlNode)
        {
            var usbPorts = new List<USBPort>();
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    var usbPort = new USBPort();
                    int.TryParse(Regex.Match(value, "(?<match>[0-9]+) x").Groups["match"].Value, out int quantity);
                    usbPort.Num = quantity;

                    if (value.ToLower().Contains("usb"))
                    {
                        if (Regex.IsMatch(value, "usb.*3\\.0.*charg.*", RegexOptions.IgnoreCase))
                        {
                            usbPort.Type = USBPortEnum.usb_3_0;
                        }
                        else if (value.ToLower().Contains("kvm"))
                        {
                            usbPort.Type = USBPortEnum.usb_KVM;
                        }
                        else
                        {
                            value = Regex.Match(value, "x (?<match>.+)").Groups["match"].Value;
                            var type = USBPortEnum.not_available;
                            value = value.Replace("Type-", "");
                            value = value.Replace(" ", "_");
                            value = value.Replace("-", "_");
                            value = value.Replace(".", "_");
                            value = value.Replace("(", "");
                            value = value.Replace(")", "");

                            if (Enum.TryParse(value, true, out type))
                            {
                                usbPort.Type = type;
                            }
                        }
                    }
                    if (usbPort.Type != 0)
                    {
                        usbPorts.Add(usbPort);
                    }
                }
            }
            return usbPorts;
        }

        private string ParseAudioIn(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (Regex.IsMatch(value, ".*audio.*in", RegexOptions.IgnoreCase))
                    {
                        return value;
                    }
                }
            }
            return "";
        }

        private string ParseAudioOut(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (Regex.IsMatch(value, ".*audio.*out", RegexOptions.IgnoreCase))
                    {
                        return value;
                    }
                }
            }
            return "";
        }

        private string ParseMicrophonePort(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (Regex.IsMatch(value, ".*microphone.*", RegexOptions.IgnoreCase))
                    {
                        return value;
                    }
                }
            }
            return "";
        }

        private bool ParseSD_Card(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (Regex.IsMatch(value, ".*memory.*card", RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ParseEthernet(HtmlNode htmlNode)
        {
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (Regex.IsMatch(value, ".*ethernet.*", RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private ICollection<Feature> ParseFeatures(HtmlNode htmlNode)
        {
            var result = new List<Feature>();
            var childNodes = htmlNode.ChildNodes.ToArray();
            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    result.Add(new Feature() { Value = value });
                }
            }
            return result;
        }

        private ICollection<CertificateStandartLicense> ParseCSL(HtmlNode htmlNode)
        {
            var result = new List<CertificateStandartLicense>();
            var childNodes = htmlNode.ChildNodes.ToArray();

            for (int currentChild = 0; currentChild < childNodes.Length; currentChild++)
            {
                var value = childNodes[currentChild].InnerText;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    result.Add(new CertificateStandartLicense() { Value = value });
                }
            }

            return result;
        }

        #endregion
    }
}
