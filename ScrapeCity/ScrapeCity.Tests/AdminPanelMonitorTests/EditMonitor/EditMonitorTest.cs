using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using ScrapeCity.Common.Enums;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Data;
using ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor.Helper;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace ScrapeCity.Tests.AdminPanelMonitorTests.EditMonitor
{
    class EditMonitorTest
    {
        IJavaScriptExecutor js;
        IWebDriver driver;
        ScrapeCityDbContext context;
        EditMonitorHelper helper;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            js = (IJavaScriptExecutor)driver;
            context = new ScrapeCityDbContext();
            helper = new EditMonitorHelper();
        }

        [Test]
        public void EditMonitor()
        {
            #region Variables
            var random = new System.Random();

            var displaySpecIdValue = "DisplaySpecId";
            double screenSizeValue = random.Next(1, 150);
            var screenDiagonalValue = random.Next(1, 150);
            var screenWidthValue = random.Next(1, 150);
            var screenHeightValue = random.Next(1, 150);
            var radiusOfCurvatureValue = random.Next(1, 150);
            PanelType[] panelTypeEnums = (PanelType[])PanelType.GetValues(typeof(PanelType));
            var panelTypeEnumsValue = panelTypeEnums[random.Next(0, panelTypeEnums.Length)];
            var panelBitDepthValue = random.Next(1, 150);
            var lutValue = random.Next(1, 150);
            var colorDepthValue = random.Next(1, 150);
            var aspectRatioValue = random.Next(1, 150);
            var aspectRatioCommonValue = random.Next(1, 100) + ":" + random.Next(1, 100);
            var maxHorizontalPixelsValue = random.Next(100, 10000);
            var maxVerticalPixelsValue = random.Next(100, 10000);
            var pixelPitchValue = random.Next(1, 150);
            var ppiValue = random.Next(1, 100);
            var displayAreaValue = random.Next(1, 150);
            Backlight[] backlightTypeEnums = (Backlight[])Backlight.GetValues(typeof(Backlight));
            var backlightTypeEnumsValue = backlightTypeEnums[random.Next(0, backlightTypeEnums.Length)];
            var ntscValue = random.Next(1, 150);
            var srgbValue = random.Next(1, 150);
            var adobeRgbValue = random.Next(1, 150);
            var dcip3Value = random.Next(1, 150);
            var rec2020Value = random.Next(1, 150);
            var brightnessValue = random.Next(1, 2000);
            var peakBrightnessValue = random.Next(1, 2000);
            var staticContrastValue = random.Next(1, 10000);
            var dynamicContrastRatioValue = random.Next(1, 999999999);
            var hdrValue = "EDITHDR";
            var horizontalViewingAngleValue = random.Next(1, 200);
            var verticalViewingAngleValue = random.Next(1, 200);
            var minResponseTimeValue = random.Next(1, 10);
            var averageResponseTimeValue = random.Next(1, 20);
            var maxResponseTimeValue = random.Next(1, 30);
            var inputLagValue = random.Next(1, 150);
            ScreenType[] screenTypeEnums = (ScreenType[])ScreenType.GetValues(typeof(ScreenType));
            var monitorScreenTypeEnumsValue = screenTypeEnums[random.Next(0, screenTypeEnums.Length)];
            var cie1976Value = random.Next(1, 150);
            var cie1931Value = random.Next(1, 150);
            var rec709Value = random.Next(1, 150);
            DisplaySyncType[] displaySyncEnums = (DisplaySyncType[])DisplaySyncType.GetValues(typeof(DisplaySyncType));
            var monitorDisplaySyncValue = displaySyncEnums[random.Next(0, displaySyncEnums.Length)];
            var minHorizontalFrequencyValue = random.Next(1, 50);
            var maxHorizontalFrequencyValue = random.Next(50, 300);
            var verticalFrequencyValue = random.Next(1, 300);
            var min110Value = random.Next(1, 200);
            var max110Value = random.Next(1, 200);
            var min220Value = random.Next(1, 300);
            var max220Value = random.Next(1, 300);
            var alternatingCurrentFrequencyMinValue = random.Next(1, 50);
            var alternatingCurrentFrequencyMaxValue = random.Next(1, 70);
            var powerConsumptionOffValue = random.Next(0, 1);
            var powerConsumptionSleepValue = random.Next(0, 2);
            var powerConsumptionAverageValue = random.Next(0, 220);
            var powerConsumptionEcoValue = random.Next(0, 85);
            var powerConsumptionMaxValue = random.Next(0, 260);
            var amperageValue = random.Next(0, 6);
            var energyEfficiencyClassValue = "EditEnergyEfficiencyClass";
            var widthValue = random.Next(0, 150);
            var heightValue = random.Next(0, 150);
            var depthValue = random.Next(0, 150);
            var weightValue = random.Next(0, 150);
            var widthWithStandValue = random.Next(0, 150);
            var heightWithStandValue = random.Next(0, 150);
            var depthWithStandValue = random.Next(0, 150);
            var weightWithStandValue = random.Next(0, 150);
            var panelColorsValue = "EditTestColor";
            var vesa_InterfaceValue = random.Next(0, 200);
            var heightAdjustmentRangeValue = random.Next(0, 200);
            var leftPivotValue = random.Next(0, 200);
            var rightPivotValue = random.Next(0, 200);
            var leftSwivelValue = random.Next(0, 200);
            var rightSwivelValue = random.Next(0, 200);
            var forwardTiltValue = random.Next(0, 200);
            var backwardTiltValue = random.Next(0, 200);
            var speakersQuantityValue = random.Next(1, 4);
            var speakersWattsyValue = random.Next(1, 10);
            var cameraImageResolutionPixelsValue = random.Next(1, 5000) + ":" + random.Next(1, 5000);
            var cameraImageResolutionMegaPixelsValue = random.Next(1, 5);
            var cameraVideoResolutionPixelsValue = random.Next(1, 5000) + ":" + random.Next(1, 5000);
            var cameraVideoResolutionMegaPixelsValue = random.Next(1, 5);
            VideoPortEnum[] videoPortEnums = (VideoPortEnum[])VideoPortEnum.GetValues(typeof(VideoPortEnum));
            var videoPortTypeValue = videoPortEnums[random.Next(0, videoPortEnums.Length)];
            var videoPortNumValue = random.Next(1, 4);
            USBPortEnum[] usbPortEnums = (USBPortEnum[])USBPortEnum.GetValues(typeof(USBPortEnum));
            var usbPortTypeValue = usbPortEnums[random.Next(0, usbPortEnums.Length)];
            var usbPortNumValue = random.Next(1, 4);
            var audioPortInValue = "EditAudioInTest x 1";
            var audioPortOutValue = "EditAudioInTest x 1";
            var microphonePortValue = "EditMicrophoneTest x 1";
            var networkValue = "EditNetworkTest";
            var featureValue = "EditTest feature";
            var cslValue = "EditTest csl";
            var unhandledValue = "EditTest unhandled";
            #endregion

            var testBrand = new Brand() { BrandName = TestSettings.brandName };
            context.Brands.Add(testBrand);
            context.SaveChanges();

            //site url
            driver.Url = TestSettings.AdminPanelUrl;

            //prepare target to edit
            var monitorToBeEdited = new Common.Models.Monitors.Monitor()
            {
                Model = TestSettings.monitorModel,
            };
            monitorToBeEdited.Brand = context.Brands.FirstOrDefault(x => x.BrandName == TestSettings.brandName);

            context.Monitors.Add(monitorToBeEdited);
            context.SaveChanges();
            context.Entry(monitorToBeEdited).State = EntityState.Detached;

            //get id of newly created monitor
            var monitorToBeEditedId = context.Monitors.FirstOrDefault(x => x.Model == TestSettings.monitorModel).Id;

            //enter account details if not logged in
            helper.EnterAccountDetails(driver);

            //go to edit
            helper.GoToEditPage(driver, TestSettings.brandName, monitorToBeEditedId);

            #region Fill form
            var displaySpecId = driver.FindElement(By.Id("DisplaySpecId"));
            displaySpecId.Clear();
            displaySpecId.SendKeys(displaySpecIdValue);

            var brand = new SelectElement(driver.FindElement(By.Name("BrandId")));
            brand.SelectByText(TestSettings.brandName);

            var model = driver.FindElement(By.Id("Model"));
            model.Clear();
            model.SendKeys(TestSettings.editedBrandName);

            var navLinks = driver.FindElements(By.CssSelector(".nav-link"));
            var displayNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Display"));
            displayNavlink.Click();

            var screenSize = driver.FindElement(By.Id("ScreenSize"));
            js.ExecuteScript("window.scrollBy(0, -100);");
            screenSize.Clear();
            screenSize.SendKeys(screenSizeValue.ToString());

            var screenDiagonal = driver.FindElement(By.Id("ScreenDiagonal"));
            screenDiagonal.Clear();
            screenDiagonal.SendKeys(screenDiagonalValue.ToString());

            var screenWidth = driver.FindElement(By.Id("ScreenWidth"));
            screenWidth.Clear();
            screenWidth.SendKeys(screenWidthValue.ToString());

            var screenHeight = driver.FindElement(By.Id("ScreenHeight"));
            screenHeight.Clear();
            screenHeight.SendKeys(screenHeightValue.ToString());

            var curvedPanel = driver.FindElement(By.Id("HasCurvedPanel"));
            if (!curvedPanel.Selected)
            {
                curvedPanel.Click();
            }

            var radiusOfCurvature = driver.FindElement(By.Id("RadiusOfCurvature"));
            radiusOfCurvature.Clear();
            radiusOfCurvature.SendKeys(radiusOfCurvatureValue.ToString());

            var panelType = new SelectElement(driver.FindElement(By.Id("PanelType")));
            panelType.SelectByText(panelTypeEnumsValue.ToString());

            var panelBitDepth = driver.FindElement(By.Id("PanelBitDepth"));
            panelBitDepth.Clear();
            panelBitDepth.SendKeys(panelBitDepthValue.ToString());

            var frc = driver.FindElement(By.Id("FRC"));
            if (!frc.Selected)
            {
                frc.Click();
            }

            var lut = driver.FindElement(By.Id("LUT"));
            lut.Clear();
            lut.SendKeys(lutValue.ToString());

            var colorDepth = driver.FindElement(By.Id("ColorDepth"));
            colorDepth.Clear();
            colorDepth.SendKeys(colorDepthValue.ToString());

            var aspectRatio = driver.FindElement(By.Id("AspectRatio"));
            aspectRatio.Clear();
            aspectRatio.SendKeys(aspectRatioValue.ToString());

            var aspectRatioCommon = driver.FindElement(By.Id("AspectRatioCommon"));
            aspectRatioCommon.Clear();
            aspectRatioCommon.SendKeys(aspectRatioCommonValue);

            var horizontalRes = driver.FindElement(By.Id("MaxHorizontalPixels"));
            horizontalRes.Clear();
            horizontalRes.SendKeys(maxHorizontalPixelsValue.ToString());

            var verticalRes = driver.FindElement(By.Id("MaxVerticalPixels"));
            verticalRes.Clear();
            verticalRes.SendKeys(maxVerticalPixelsValue.ToString());

            var pixelPitch = driver.FindElement(By.Id("PixelPitch"));
            pixelPitch.Clear();
            pixelPitch.SendKeys(pixelPitchValue.ToString());

            var ppi = driver.FindElement(By.Id("PPI"));
            ppi.Clear();
            ppi.SendKeys(ppiValue.ToString());

            var displayArea = driver.FindElement(By.Id("DisplayArea"));
            displayArea.Clear();
            displayArea.SendKeys(displayAreaValue.ToString());

            var backlight = new SelectElement(driver.FindElement(By.Id("BacklightType")));
            backlight.SelectByText(backlightTypeEnumsValue.ToString());

            var ntsc = driver.FindElement(By.Id("NTSC"));
            ntsc.Clear();
            ntsc.SendKeys(ntscValue.ToString());

            var sRGB = driver.FindElement(By.Id("sRGB"));
            sRGB.Clear();
            sRGB.SendKeys(srgbValue.ToString());

            var adobeRGB = driver.FindElement(By.Id("AdobeRGB"));
            adobeRGB.Clear();
            adobeRGB.SendKeys(adobeRgbValue.ToString());

            var dcip3 = driver.FindElement(By.Id("DCI_P3"));
            dcip3.Clear();
            dcip3.SendKeys(dcip3Value.ToString());

            var rec2020 = driver.FindElement(By.Id("Rec2020"));
            rec2020.Clear();
            rec2020.SendKeys(rec2020Value.ToString());

            var brightness = driver.FindElement(By.Id("Brightness"));
            brightness.Clear();
            brightness.SendKeys(brightnessValue.ToString());

            var peakBrightness = driver.FindElement(By.Id("PeakBrightness"));
            peakBrightness.Clear();
            peakBrightness.SendKeys(peakBrightnessValue.ToString());

            var staticContrast = driver.FindElement(By.Id("StaticContrast"));
            staticContrast.Clear();
            staticContrast.SendKeys(staticContrastValue.ToString());

            var dynamicContrastRatio = driver.FindElement(By.Id("DynamicContrast"));
            dynamicContrastRatio.Clear();
            dynamicContrastRatio.SendKeys(dynamicContrastRatioValue.ToString());

            var hdr = driver.FindElement(By.Id("HDR"));
            hdr.Clear();
            hdr.SendKeys(hdrValue);

            var horizontalViewingAngle = driver.FindElement(By.Id("HorizontalViewingAngle"));
            horizontalViewingAngle.Clear();
            horizontalViewingAngle.SendKeys(horizontalViewingAngleValue.ToString());

            var verticalViewingAngle = driver.FindElement(By.Id("VerticalViewingAngle"));
            verticalViewingAngle.Clear();
            verticalViewingAngle.SendKeys(verticalViewingAngleValue.ToString());

            var minResponseTime = driver.FindElement(By.Id("MinimumResponceTime"));
            minResponseTime.Clear();
            minResponseTime.SendKeys(minResponseTimeValue.ToString());

            var averageResponseTime = driver.FindElement(By.Id("AverageResponceTime"));
            averageResponseTime.Clear();
            averageResponseTime.SendKeys(averageResponseTimeValue.ToString());

            var maxResponseTime = driver.FindElement(By.Id("MaximumResponceTime"));
            maxResponseTime.Clear();
            maxResponseTime.SendKeys(maxResponseTimeValue.ToString());

            var inputLag = driver.FindElement(By.Id("InputLag"));
            inputLag.Clear();
            inputLag.SendKeys(inputLagValue.ToString());

            var screenType = new SelectElement(driver.FindElement(By.Id("ScreenType")));
            screenType.SelectByText(monitorScreenTypeEnumsValue.ToString());

            var cie1976 = driver.FindElement(By.Id("CIE1976"));
            cie1976.Clear();
            cie1976.SendKeys(cie1976Value.ToString());

            var cie1931 = driver.FindElement(By.Id("CIE1931"));
            cie1931.Clear();
            cie1931.SendKeys(cie1931Value.ToString());

            var rec709 = driver.FindElement(By.Id("REC709"));
            rec709.Clear();
            rec709.SendKeys(rec709Value.ToString());

            var displaySyncDropdown = new SelectElement(driver.FindElement(By.Id("DisplaySyncType")));
            displaySyncDropdown.SelectByText(monitorDisplaySyncValue.ToString());

            var threeD = driver.FindElement(By.Id("ThreeD"));
            if (!threeD.Selected)
            {
                threeD.Click();
            }

            var frequenciesNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Frequencies"));
            frequenciesNavlink.Click();

            var minHorizontalFrequency = driver.FindElement(By.Id("MinHorizontalFrequency"));
            minHorizontalFrequency.Clear();
            minHorizontalFrequency.SendKeys(minHorizontalFrequencyValue.ToString());

            var maxHorizontalFrequency = driver.FindElement(By.Id("MaxHorizontalFrequency"));
            maxHorizontalFrequency.Clear();
            maxHorizontalFrequency.SendKeys(maxHorizontalFrequencyValue.ToString());

            var verticalFrequency = driver.FindElement(By.Id("VerticalFrequency"));
            verticalFrequency.Clear();
            verticalFrequency.SendKeys(verticalFrequencyValue.ToString());

            var min110 = driver.FindElement(By.Id("Minimum110V"));
            min110.Clear();
            min110.SendKeys(min110Value.ToString());

            var max110 = driver.FindElement(By.Id("Maximum110V"));
            max110.Clear();
            max110.SendKeys(max110Value.ToString());

            var min220 = driver.FindElement(By.Id("Minimum220V"));
            min220.Clear();
            min220.SendKeys(min220Value.ToString());

            var max220 = driver.FindElement(By.Id("Maximum220V"));
            max220.Clear();
            max220.SendKeys(max220Value.ToString());

            var alternatingCurrentFrequencyMin = driver.FindElement(By.Id("AlternatingCurrentFrequencyMin"));
            alternatingCurrentFrequencyMin.Clear();
            alternatingCurrentFrequencyMin.SendKeys(alternatingCurrentFrequencyMinValue.ToString());

            var alternatingCurrentFrequencyMax = driver.FindElement(By.Id("AlternatingCurrentFrequencyMax"));
            alternatingCurrentFrequencyMax.Clear();
            alternatingCurrentFrequencyMax.SendKeys(alternatingCurrentFrequencyMaxValue.ToString());

            var powerConsumptionNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("consumption"));
            js.ExecuteScript("window.scrollBy(0, -150);");
            powerConsumptionNavlink.Click();

            var powerConsumptionOff = driver.FindElement(By.Id("PowerConsumptionOff"));
            powerConsumptionOff.Clear();
            powerConsumptionOff.SendKeys(powerConsumptionOffValue.ToString());

            var powerConsumptionSleep = driver.FindElement(By.Id("PowerConsumptionSleep"));
            powerConsumptionSleep.Clear();
            powerConsumptionSleep.SendKeys(powerConsumptionSleepValue.ToString());

            var powerConsumptionAverage = driver.FindElement(By.Id("PowerConsumptionAverage"));
            powerConsumptionAverage.Clear();
            powerConsumptionAverage.SendKeys(powerConsumptionAverageValue.ToString());

            var powerConsumptionEco = driver.FindElement(By.Id("PowerConsumptionEco"));
            powerConsumptionEco.Clear();
            powerConsumptionEco.SendKeys(powerConsumptionEcoValue.ToString());

            var powerConsumptionMax = driver.FindElement(By.Id("PowerConsumptionMaximum"));
            powerConsumptionMax.Clear();
            powerConsumptionMax.SendKeys(powerConsumptionMaxValue.ToString());

            var amperage = driver.FindElement(By.Id("Amperage"));
            amperage.Clear();
            amperage.SendKeys(amperageValue.ToString());

            var energyEfficiencyClass = driver.FindElement(By.Id("EnergyEfficiencyClass"));
            energyEfficiencyClass.Clear();
            energyEfficiencyClass.SendKeys(energyEfficiencyClassValue);

            var dwcNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Dimensions"));
            dwcNavlink.Click();

            var width = driver.FindElement(By.Id("Width"));
            width.Clear();
            width.SendKeys(widthValue.ToString());

            var height = driver.FindElement(By.Id("Height"));
            height.Clear();
            height.SendKeys(heightValue.ToString());

            var depth = driver.FindElement(By.Id("Depth"));
            depth.Clear();
            depth.SendKeys(depthValue.ToString());

            var weight = driver.FindElement(By.Id("Weight"));
            weight.Clear();
            weight.SendKeys(weightValue.ToString());

            var widthWithStand = driver.FindElement(By.Id("WidthWithStand"));
            widthWithStand.Clear();
            widthWithStand.SendKeys(widthWithStandValue.ToString());

            var heightWithStand = driver.FindElement(By.Id("HeightWithStand"));
            heightWithStand.Clear();
            heightWithStand.SendKeys(heightWithStandValue.ToString());

            var depthWithStand = driver.FindElement(By.Id("DepthWithStand"));
            depthWithStand.Clear();
            depthWithStand.SendKeys(depthWithStandValue.ToString());

            var weightWithStand = driver.FindElement(By.Id("WeightWithStand"));
            weightWithStand.Clear();
            weightWithStand.SendKeys(weightWithStandValue.ToString());

            var panelColorsAddInputField = driver.FindElement(By.Id("PanelColorsAddInputField"));
            panelColorsAddInputField.Click();
            var panelColors = driver.FindElements(By.Name("PanelColors"));
            foreach (var panelColor in panelColors)
            {
                if (panelColor.TagName == "input")
                {
                    panelColor.Clear();
                    panelColor.SendKeys(panelColorsValue);
                }
            }

            var ergonomicsNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Ergonomics"));
            js.ExecuteScript("window.scrollBy(0, -150);");
            ergonomicsNavlink.Click();

            var vesa_Mount = driver.FindElement(By.Id("VESA_Mount"));
            if (!vesa_Mount.Selected)
            {
                js.ExecuteScript("window.scrollBy(0, -150);");
                vesa_Mount.Click();
            }

            var vesa_Interface = driver.FindElement(By.Id("VESA_Interface"));
            vesa_Interface.Clear();
            vesa_Interface.SendKeys(vesa_InterfaceValue.ToString());

            var removeableStand = driver.FindElement(By.Id("RemoveableStand"));
            if (!removeableStand.Selected)
            {
                removeableStand.Click();
            }

            var heightAdjustable = driver.FindElement(By.Id("HeightAdjustable"));
            if (!heightAdjustable.Selected)
            {
                heightAdjustable.Click();
            }

            var heightAdjustmentRange = driver.FindElement(By.Id("HeightAdjustmentRange"));
            heightAdjustmentRange.Clear();
            heightAdjustmentRange.SendKeys(heightAdjustmentRangeValue.ToString());

            var landscapePortraitPivot = driver.FindElement(By.Id("LandscapePortraitPivot"));
            if (!landscapePortraitPivot.Selected)
            {
                landscapePortraitPivot.Click();
            }

            var leftPivot = driver.FindElement(By.Id("LeftPivot"));
            leftPivot.Clear();
            leftPivot.SendKeys(leftPivotValue.ToString());

            var rightPivot = driver.FindElement(By.Id("RightPivot"));
            rightPivot.Clear();
            rightPivot.SendKeys(rightPivotValue.ToString());

            var leftRightSwivel = driver.FindElement(By.Id("LeftRightSwivel"));
            if (!leftRightSwivel.Selected)
            {
                leftRightSwivel.Click();
            }

            var leftSwivel = driver.FindElement(By.Id("LeftSwivel"));
            leftSwivel.Clear();
            leftSwivel.SendKeys(leftSwivelValue.ToString());

            var rightSwivel = driver.FindElement(By.Id("RightSwivel"));
            rightSwivel.Clear();
            rightSwivel.SendKeys(rightSwivelValue.ToString());

            var forwardBackwardTilt = driver.FindElement(By.Id("ForwardBackwardTilt"));
            if (!forwardBackwardTilt.Selected)
            {
                forwardBackwardTilt.Click();
            }

            var forwardTilt = driver.FindElement(By.Id("ForwardTilt"));
            forwardTilt.Clear();
            forwardTilt.SendKeys(forwardTiltValue.ToString());

            var backwardTilt = driver.FindElement(By.Id("BackwardTilt"));
            backwardTilt.Clear();
            backwardTilt.SendKeys(backwardTiltValue.ToString());

            var audioNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Audio"));
            audioNavlink.Click();

            var hasSpeakers = driver.FindElement(By.CssSelector("[id*=Speakers_HasSpeakers]"));
            if (!hasSpeakers.Selected)
            {
                js.ExecuteScript("window.scrollBy(0, -150);");
                hasSpeakers.Click();
            }

            var speakersQuantity = driver.FindElement(By.CssSelector("[id*=Speakers_Quantity]"));
            speakersQuantity.Clear();
            speakersQuantity.SendKeys(speakersQuantityValue.ToString());

            var speakersWatts = driver.FindElement(By.CssSelector("[id*=Speakers_Watts]"));
            speakersWatts.Clear();
            speakersWatts.SendKeys(speakersWattsyValue.ToString());

            var cameraNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Camera"));
            cameraNavlink.Click();

            var hasCamera = driver.FindElement(By.CssSelector("[id*=Camera_HasCamera]"));
            if (!hasCamera.Selected)
            {
                hasCamera.Click();
            }

            var cameraImageResolutionPixels = driver.FindElement(By.CssSelector("[id*=Camera_ImageResolutionPixels]"));
            cameraImageResolutionPixels.Clear();
            cameraImageResolutionPixels.SendKeys(cameraImageResolutionPixelsValue.ToString());

            var cameraImageResolutionMegaPixels = driver.FindElement(By.CssSelector("[id*=Camera_ImageResolutionMegaPixels]"));
            cameraImageResolutionMegaPixels.Clear();
            cameraImageResolutionMegaPixels.SendKeys(cameraImageResolutionMegaPixelsValue.ToString());

            var cameraVideoResolutionPixels = driver.FindElement(By.CssSelector("[id*=Camera_VideoResolutionPixels]"));
            cameraVideoResolutionPixels.Clear();
            cameraVideoResolutionPixels.SendKeys(cameraVideoResolutionPixelsValue.ToString());

            var cameraVideoResolutionMegaPixels = driver.FindElement(By.CssSelector("[id*=Camera_VideoResolutionMegaPixels]"));
            cameraVideoResolutionMegaPixels.Clear();
            cameraVideoResolutionMegaPixels.SendKeys(cameraVideoResolutionMegaPixelsValue.ToString());

            var connectivityNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Connectivity"));
            connectivityNavlink.Click();

            var videoPortResetButton = driver.FindElement(By.Id("videoPortResetButton"));
            videoPortResetButton.Click();
            var videoPortButton = driver.FindElement(By.Id("videoPortButton"));
            videoPortButton.Click();
            var videoPortsType = new SelectElement(driver.FindElement(By.Name("videoPorts[0].Type")));
            videoPortsType.SelectByText(videoPortTypeValue.ToString());
            var videoPortsNum = driver.FindElement(By.Name("videoPorts[0].Num"));
            videoPortsNum.Clear();
            videoPortsNum.SendKeys(videoPortNumValue.ToString());

            var usbPortResetButton = driver.FindElement(By.Id("usbPortResetButton"));
            usbPortResetButton.Click();
            var usbPortButton = driver.FindElement(By.Id("usbPortButton"));
            usbPortButton.Click();
            var usbPortsType = new SelectElement(driver.FindElement(By.Name("usbPorts[0].Type")));
            usbPortsType.SelectByText(usbPortTypeValue.ToString());
            var usbPortsNum = driver.FindElement(By.Name("usbPorts[0].Num"));
            usbPortsNum.Clear();
            usbPortsNum.SendKeys(usbPortNumValue.ToString());

            var audioPortIn = driver.FindElement(By.Id("AudioPortIn"));
            audioPortIn.Clear();
            audioPortIn.SendKeys(audioPortInValue);

            var audioPortOut = driver.FindElement(By.Id("AudioPortOut"));
            audioPortOut.Clear();
            audioPortOut.SendKeys(audioPortOutValue);

            var microphonePort = driver.FindElement(By.Id("MicrophonePort"));
            microphonePort.Clear();
            microphonePort.SendKeys(microphonePortValue);

            var sdMemoryCardSlot = driver.FindElement(By.Id("SD_MemoryCardSlot"));
            if (!sdMemoryCardSlot.Selected)
            {
                sdMemoryCardSlot.Click();
            }

            var ethernetrj45 = driver.FindElement(By.Id("EthernetRJ45"));
            if (!ethernetrj45.Selected)
            {
                ethernetrj45.Click();
            }

            var network = driver.FindElement(By.Id("Network"));
            network.Clear();
            network.SendKeys(networkValue);

            var featuresNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Features"));
            featuresNavlink.Click();

            var featuresAddInputField = driver.FindElement(By.Id("addFeatureInput"));
            js.ExecuteScript("window.scrollBy(0, -150);");
            featuresAddInputField.Click();
            var features = driver.FindElements(By.Name("Features"));
            foreach (var feature in features)
            {
                if (feature.TagName == "input")
                {
                    feature.Clear();
                    feature.SendKeys(featureValue);
                }
            }

            var cslNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("Certificates"));
            cslNavlink.Click();

            var cslInput = driver.FindElement(By.Id("addCslInput"));
            cslInput.Click();
            var csls = driver.FindElements(By.Name("CertificatesStandartsLicenses"));
            foreach (var csl in csls)
            {
                if (csl.TagName == "input")
                {
                    csl.Clear();
                    csl.SendKeys(cslValue);
                }
            }

            var unhandledNavlink = navLinks.FirstOrDefault(x => x.GetProperty("href").Contains("UnhandledDisplaySpecificationProperties"));
            unhandledNavlink.Click();

            var unhandledInput = driver.FindElement(By.Id("unhandledAddInput"));
            unhandledInput.Click();
            var unhandled = driver.FindElements(By.Name("UnhandledDisplaySpecificationProperties"));
            foreach (var u in unhandled)
            {
                if (u.TagName == "input")
                {
                    u.Clear();
                    u.SendKeys(unhandledValue);
                }
            }
            #endregion

            var submit = driver.FindElement(By.Id("submit"));
            submit.Submit();

            Thread.Sleep(3000);
            var monitor = context.Monitors.Include(x => x.Speakers).Include(x => x.Camera).FirstOrDefault(x => x.Model == TestSettings.editedBrandName);
            context.Entry(monitor).State = EntityState.Modified;
            context.Entry(monitor).Reload();

            Assert.AreEqual(monitor.DisplaySpecId, displaySpecIdValue);
            Assert.AreEqual(monitor.Brand.BrandName, TestSettings.brandName);
            Assert.AreEqual(monitor.Model, TestSettings.editedBrandName);
            Assert.AreEqual(monitor.ScreenSize, screenSizeValue);
            Assert.AreEqual(monitor.ScreenDiagonal, screenDiagonalValue);
            Assert.AreEqual(monitor.ScreenWidth, screenWidthValue);
            Assert.AreEqual(monitor.ScreenHeight, screenHeightValue);
            Assert.True(monitor.HasCurvedPanel);
            Assert.AreEqual(monitor.RadiusOfCurvature, radiusOfCurvatureValue);
            Assert.AreEqual(monitor.PanelType, panelTypeEnumsValue);
            Assert.AreEqual(monitor.PanelBitDepth, panelBitDepthValue);
            Assert.True(monitor.FRC);
            Assert.AreEqual(monitor.LUT, lutValue);
            Assert.AreEqual(monitor.ColorDepth, colorDepthValue);
            Assert.AreEqual(monitor.AspectRatio, aspectRatioValue);
            Assert.AreEqual(monitor.AspectRatioCommon, aspectRatioCommonValue);
            Assert.AreEqual(monitor.MaxHorizontalPixels, maxHorizontalPixelsValue);
            Assert.AreEqual(monitor.MaxVerticalPixels, maxVerticalPixelsValue);
            Assert.AreEqual(monitor.PixelPitch, pixelPitchValue);
            Assert.AreEqual(monitor.PPI, ppiValue);
            Assert.AreEqual(monitor.DisplayArea, displayAreaValue);
            Assert.AreEqual(monitor.BacklightType, backlightTypeEnumsValue);
            Assert.AreEqual(monitor.NTSC, ntscValue);
            Assert.AreEqual(monitor.sRGB, srgbValue);
            Assert.AreEqual(monitor.AdobeRGB, adobeRgbValue);
            Assert.AreEqual(monitor.DCI_P3, dcip3Value);
            Assert.AreEqual(monitor.Rec2020, rec2020Value);
            Assert.AreEqual(monitor.Brightness, brightnessValue);
            Assert.AreEqual(monitor.PeakBrightness, peakBrightnessValue);
            Assert.AreEqual(monitor.StaticContrast, staticContrastValue);
            Assert.AreEqual(monitor.DynamicContrast, dynamicContrastRatioValue);
            Assert.AreEqual(monitor.HDR, hdrValue);
            Assert.AreEqual(monitor.HorizontalViewingAngle, horizontalViewingAngleValue);
            Assert.AreEqual(monitor.VerticalViewingAngle, verticalViewingAngleValue);
            Assert.AreEqual(monitor.MinimumResponceTime, minResponseTimeValue);
            Assert.AreEqual(monitor.AverageResponceTime, averageResponseTimeValue);
            Assert.AreEqual(monitor.MaximumResponceTime, maxResponseTimeValue);
            Assert.AreEqual(monitor.InputLag, inputLagValue);
            Assert.AreEqual(monitor.ScreenType, monitorScreenTypeEnumsValue);
            Assert.AreEqual(monitor.CIE1976, cie1976Value);
            Assert.AreEqual(monitor.CIE1931, cie1931Value);
            Assert.AreEqual(monitor.REC709, rec709Value);
            Assert.True(monitor.ThreeD);
            Assert.AreEqual(monitor.DisplaySyncType, monitorDisplaySyncValue);
            Assert.AreEqual(monitor.MinHorizontalFrequency, minHorizontalFrequencyValue);
            Assert.AreEqual(monitor.MaxHorizontalFrequency, maxHorizontalFrequencyValue);
            Assert.AreEqual(monitor.VerticalFrequency, verticalFrequencyValue);
            Assert.AreEqual(monitor.Minimum110V, min110Value);
            Assert.AreEqual(monitor.Maximum110V, max110Value);
            Assert.AreEqual(monitor.Minimum220V, min220Value);
            Assert.AreEqual(monitor.Maximum220V, max220Value);
            Assert.AreEqual(monitor.AlternatingCurrentFrequencyMin, alternatingCurrentFrequencyMinValue);
            Assert.AreEqual(monitor.AlternatingCurrentFrequencyMax, alternatingCurrentFrequencyMaxValue);
            Assert.AreEqual(monitor.PowerConsumptionOff, powerConsumptionOffValue);
            Assert.AreEqual(monitor.PowerConsumptionSleep, powerConsumptionSleepValue);
            Assert.AreEqual(monitor.PowerConsumptionAverage, powerConsumptionAverageValue);
            Assert.AreEqual(monitor.PowerConsumptionEco, powerConsumptionEcoValue);
            Assert.AreEqual(monitor.PowerConsumptionMaximum, powerConsumptionMaxValue);
            Assert.AreEqual(monitor.Amperage, amperageValue);
            Assert.AreEqual(monitor.EnergyEfficiencyClass, energyEfficiencyClassValue);
            Assert.AreEqual(monitor.Width, widthValue);
            Assert.AreEqual(monitor.Height, heightValue);
            Assert.AreEqual(monitor.Depth, depthValue);
            Assert.AreEqual(monitor.Weight, weightValue);
            Assert.AreEqual(monitor.WidthWithStand, widthWithStandValue);
            Assert.AreEqual(monitor.HeightWithStand, heightWithStandValue);
            Assert.AreEqual(monitor.DepthWithStand, depthWithStandValue);
            Assert.AreEqual(monitor.WeightWithStand, weightWithStandValue);
            Assert.That(monitor.PanelColors.Any(x => x.Value == panelColorsValue));
            Assert.True(monitor.VESA_Mount);
            Assert.AreEqual(monitor.VESA_Interface, vesa_InterfaceValue);
            Assert.True(monitor.RemoveableStand);
            Assert.True(monitor.HeightAdjustable);
            Assert.AreEqual(monitor.HeightAdjustmentRange, heightAdjustmentRangeValue);
            Assert.True(monitor.LandscapePortraitPivot);
            Assert.AreEqual(monitor.LeftPivot, leftPivotValue);
            Assert.AreEqual(monitor.RightPivot, rightPivotValue);
            Assert.True(monitor.LeftRightSwivel);
            Assert.AreEqual(monitor.LeftSwivel, leftSwivelValue);
            Assert.AreEqual(monitor.RightSwivel, rightSwivelValue);
            Assert.True(monitor.ForwardBackwardTilt);
            Assert.AreEqual(monitor.ForwardTilt, forwardTiltValue);
            Assert.AreEqual(monitor.BackwardTilt, backwardTiltValue);
            Assert.True(monitor.Speakers.HasSpeakers);
            Assert.AreEqual(monitor.Speakers.Quantity, speakersQuantityValue);
            Assert.AreEqual(monitor.Speakers.Watts, speakersWattsyValue);
            Assert.True(monitor.Camera.HasCamera);
            Assert.AreEqual(monitor.Camera.ImageResolutionPixels, cameraImageResolutionPixelsValue);
            Assert.AreEqual(monitor.Camera.ImageResolutionMegaPixels, cameraImageResolutionMegaPixelsValue);
            Assert.AreEqual(monitor.Camera.VideoResolutionPixels, cameraVideoResolutionPixelsValue);
            Assert.AreEqual(monitor.Camera.VideoResolutionMegaPixels, cameraVideoResolutionMegaPixelsValue);
            Assert.That(monitor.VideoPorts.Any(x => x.Type == videoPortTypeValue && x.Num == videoPortNumValue));
            Assert.That(monitor.USBPorts.Any(x => x.Type == usbPortTypeValue && x.Num == usbPortNumValue));
            Assert.AreEqual(monitor.AudioPortIn, audioPortInValue);
            Assert.AreEqual(monitor.AudioPortOut, audioPortOutValue);
            Assert.AreEqual(monitor.MicrophonePort, microphonePortValue);
            Assert.True(monitor.SD_MemoryCardSlot);
            Assert.True(monitor.EthernetRJ45);
            Assert.That(monitor.Features.Any(x => x.Value == featureValue));
            Assert.That(monitor.CertificatesStandartsLicenses.Any(x => x.Value == cslValue));
            Assert.That(monitor.UnhandledDisplaySpecificationProperties.Any(x => x.Value == unhandledValue));
        }

        [TearDown]
        public void EndTest()
        {
            ScreenshotHandler.MakeScreenshot(TestContext.CurrentContext, driver);

            driver.Close();

            foreach (var monitor in context.Monitors.Where(x => x.Model == TestSettings.monitorModel || x.Model == TestSettings.monitorEditedModel))
            {
                context.Monitors.Remove(monitor);
            }
            foreach (var brand in context.Brands.Where(x => x.BrandName == TestSettings.brandName))
            {
                context.Brands.Remove(brand);
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}
