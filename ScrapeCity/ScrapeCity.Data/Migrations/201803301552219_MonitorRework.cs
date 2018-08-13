namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonitorRework : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasCamera = c.Boolean(nullable: false),
                        ImageResolutionPixels = c.Int(nullable: false),
                        ImageResolutionMegaPixels = c.Double(nullable: false),
                        VideoResolutionPixels = c.Int(nullable: false),
                        VideoResolutionMegaPixels = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CertificatesStandartsLicenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            CreateTable(
                "dbo.PanelColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Watts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThreeDs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            AddColumn("dbo.Monitors", "DisplaySpecId", c => c.String());
            AddColumn("dbo.Monitors", "ScreenDiagonal", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "ScreenWidth", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "ScreenHeight", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "RadiusOfCurvature", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "PanelBitDepth", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "FRC", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "LUT", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "AspectRatioCommon", c => c.String());
            AddColumn("dbo.Monitors", "PixelPitch", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "DisplayArea", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "NTSC", c => c.String());
            AddColumn("dbo.Monitors", "DCI_P3", c => c.String());
            AddColumn("dbo.Monitors", "Rec2020", c => c.String());
            AddColumn("dbo.Monitors", "PeakBrightness", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "StaticContrast", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "DynamicContrast", c => c.Long(nullable: false));
            AddColumn("dbo.Monitors", "HDR", c => c.String());
            AddColumn("dbo.Monitors", "MinimumResponceTime", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "AverageResponceTime", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "MaximumResponceTime", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "InputLag", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "HorizontalFrequency", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "VerticalFrequency", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "Minimum110V", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "Maximum110V", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "Minimum220V", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "Maximum220V", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "AlternatingCurrentFrequencyMin", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "AlternatingCurrentFrequencyMax", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionOff", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionSleep", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionAverage", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionEco", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionMaximum", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "Amperage", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "EnergyEfficiencyClass", c => c.String());
            AddColumn("dbo.Monitors", "Width", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "Height", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "Depth", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "WidthWithStand", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "HeightWithStand", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "DepthWithStand", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "WeightWithStand", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "VESA_Mount", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "VESA_Interface", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "RemoveableStand", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "HeightAdjustmentRange", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "LandscapePortraitPivot", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "LeftPivot", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "RightPivot", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "LeftRightSwivel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "LeftSwivel", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "RightSwivel", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "ForwardBackwardTilt", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "ForwardTilt", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "BackwardTilt", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "Network", c => c.String());
            AddColumn("dbo.Monitors", "AudioPortIn", c => c.String());
            AddColumn("dbo.Monitors", "AudioPortOut", c => c.String());
            AddColumn("dbo.Monitors", "MicrophonePort", c => c.String());
            AddColumn("dbo.Monitors", "SD_MemoryCardSlot", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "EthernetRJ45", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "Camera_Id", c => c.Int());
            AddColumn("dbo.Monitors", "Speakers_Id", c => c.Int());
            AlterColumn("dbo.Monitors", "AdobeRGB", c => c.String());
            AlterColumn("dbo.Monitors", "sRGB", c => c.String());
            CreateIndex("dbo.Monitors", "Camera_Id");
            CreateIndex("dbo.Monitors", "Speakers_Id");
            AddForeignKey("dbo.Monitors", "Camera_Id", "dbo.Cameras", "Id");
            AddForeignKey("dbo.Monitors", "Speakers_Id", "dbo.Speakers", "Id");
            DropColumn("dbo.Monitors", "MonitorURL");
            DropColumn("dbo.Monitors", "UPC");
            DropColumn("dbo.Monitors", "ResponseTime");
            DropColumn("dbo.Monitors", "MaxFrequencyAtMaxResolution");
            DropColumn("dbo.Monitors", "CurvedPanel");
            DropColumn("dbo.Monitors", "ContrastRatio");
            DropColumn("dbo.Monitors", "DynamicContrastRatio");
            DropColumn("dbo.Monitors", "PowerConsumptionMax");
            DropColumn("dbo.Monitors", "PowerConsumptionMin");
            DropColumn("dbo.Monitors", "VesaMount");
            DropColumn("dbo.Monitors", "VesaHoleDistance");
            DropColumn("dbo.Monitors", "MTBF");
            DropColumn("dbo.Monitors", "FlickerFree");
            DropColumn("dbo.Monitors", "TouchScreen");
            DropColumn("dbo.Monitors", "LowBlueLight");
            DropColumn("dbo.Monitors", "HDCP_Support");
            DropColumn("dbo.Monitors", "PictureByPicture");
            DropColumn("dbo.Monitors", "PictureInPicture");
            DropColumn("dbo.Monitors", "CardReader");
            DropColumn("dbo.Monitors", "MHL");
            DropColumn("dbo.Monitors", "KensingtonLock");
            DropColumn("dbo.Monitors", "MacCompatable");
            DropColumn("dbo.Monitors", "KVM_Switch");
            DropColumn("dbo.Monitors", "AudioIn");
            DropColumn("dbo.Monitors", "AudioOut");
            DropColumn("dbo.Monitors", "Tilt");
            DropColumn("dbo.Monitors", "Swivel");
            DropColumn("dbo.Monitors", "Pivot");
            DropColumn("dbo.Monitors", "Speakers");
            DropColumn("dbo.Monitors", "AutoBrightness");
            DropColumn("dbo.Monitors", "EnergyStar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Monitors", "EnergyStar", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "AutoBrightness", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "Speakers", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "Pivot", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "Swivel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "Tilt", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "AudioOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "AudioIn", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "KVM_Switch", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "MacCompatable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "KensingtonLock", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "MHL", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "CardReader", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "PictureInPicture", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "PictureByPicture", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "HDCP_Support", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "LowBlueLight", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "TouchScreen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "FlickerFree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "MTBF", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "VesaHoleDistance", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "VesaMount", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionMin", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "PowerConsumptionMax", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "DynamicContrastRatio", c => c.Long(nullable: false));
            AddColumn("dbo.Monitors", "ContrastRatio", c => c.String());
            AddColumn("dbo.Monitors", "CurvedPanel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Monitors", "MaxFrequencyAtMaxResolution", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "ResponseTime", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "UPC", c => c.String());
            AddColumn("dbo.Monitors", "MonitorURL", c => c.String());
            DropForeignKey("dbo.ThreeDs", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.Monitors", "Speakers_Id", "dbo.Speakers");
            DropForeignKey("dbo.PanelColors", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.Features", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.CertificatesStandartsLicenses", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.Monitors", "Camera_Id", "dbo.Cameras");
            DropIndex("dbo.ThreeDs", new[] { "MonitorId" });
            DropIndex("dbo.PanelColors", new[] { "MonitorId" });
            DropIndex("dbo.Features", new[] { "MonitorId" });
            DropIndex("dbo.CertificatesStandartsLicenses", new[] { "MonitorId" });
            DropIndex("dbo.Monitors", new[] { "Speakers_Id" });
            DropIndex("dbo.Monitors", new[] { "Camera_Id" });
            AlterColumn("dbo.Monitors", "sRGB", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Monitors", "AdobeRGB", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Monitors", "Speakers_Id");
            DropColumn("dbo.Monitors", "Camera_Id");
            DropColumn("dbo.Monitors", "EthernetRJ45");
            DropColumn("dbo.Monitors", "SD_MemoryCardSlot");
            DropColumn("dbo.Monitors", "MicrophonePort");
            DropColumn("dbo.Monitors", "AudioPortOut");
            DropColumn("dbo.Monitors", "AudioPortIn");
            DropColumn("dbo.Monitors", "Network");
            DropColumn("dbo.Monitors", "BackwardTilt");
            DropColumn("dbo.Monitors", "ForwardTilt");
            DropColumn("dbo.Monitors", "ForwardBackwardTilt");
            DropColumn("dbo.Monitors", "RightSwivel");
            DropColumn("dbo.Monitors", "LeftSwivel");
            DropColumn("dbo.Monitors", "LeftRightSwivel");
            DropColumn("dbo.Monitors", "RightPivot");
            DropColumn("dbo.Monitors", "LeftPivot");
            DropColumn("dbo.Monitors", "LandscapePortraitPivot");
            DropColumn("dbo.Monitors", "HeightAdjustmentRange");
            DropColumn("dbo.Monitors", "RemoveableStand");
            DropColumn("dbo.Monitors", "VESA_Interface");
            DropColumn("dbo.Monitors", "VESA_Mount");
            DropColumn("dbo.Monitors", "WeightWithStand");
            DropColumn("dbo.Monitors", "DepthWithStand");
            DropColumn("dbo.Monitors", "HeightWithStand");
            DropColumn("dbo.Monitors", "WidthWithStand");
            DropColumn("dbo.Monitors", "Weight");
            DropColumn("dbo.Monitors", "Depth");
            DropColumn("dbo.Monitors", "Height");
            DropColumn("dbo.Monitors", "Width");
            DropColumn("dbo.Monitors", "EnergyEfficiencyClass");
            DropColumn("dbo.Monitors", "Amperage");
            DropColumn("dbo.Monitors", "PowerConsumptionMaximum");
            DropColumn("dbo.Monitors", "PowerConsumptionEco");
            DropColumn("dbo.Monitors", "PowerConsumptionAverage");
            DropColumn("dbo.Monitors", "PowerConsumptionSleep");
            DropColumn("dbo.Monitors", "PowerConsumptionOff");
            DropColumn("dbo.Monitors", "AlternatingCurrentFrequencyMax");
            DropColumn("dbo.Monitors", "AlternatingCurrentFrequencyMin");
            DropColumn("dbo.Monitors", "Maximum220V");
            DropColumn("dbo.Monitors", "Minimum220V");
            DropColumn("dbo.Monitors", "Maximum110V");
            DropColumn("dbo.Monitors", "Minimum110V");
            DropColumn("dbo.Monitors", "VerticalFrequency");
            DropColumn("dbo.Monitors", "HorizontalFrequency");
            DropColumn("dbo.Monitors", "InputLag");
            DropColumn("dbo.Monitors", "MaximumResponceTime");
            DropColumn("dbo.Monitors", "AverageResponceTime");
            DropColumn("dbo.Monitors", "MinimumResponceTime");
            DropColumn("dbo.Monitors", "HDR");
            DropColumn("dbo.Monitors", "DynamicContrast");
            DropColumn("dbo.Monitors", "StaticContrast");
            DropColumn("dbo.Monitors", "PeakBrightness");
            DropColumn("dbo.Monitors", "Rec2020");
            DropColumn("dbo.Monitors", "DCI_P3");
            DropColumn("dbo.Monitors", "NTSC");
            DropColumn("dbo.Monitors", "DisplayArea");
            DropColumn("dbo.Monitors", "PixelPitch");
            DropColumn("dbo.Monitors", "AspectRatioCommon");
            DropColumn("dbo.Monitors", "LUT");
            DropColumn("dbo.Monitors", "FRC");
            DropColumn("dbo.Monitors", "PanelBitDepth");
            DropColumn("dbo.Monitors", "RadiusOfCurvature");
            DropColumn("dbo.Monitors", "ScreenHeight");
            DropColumn("dbo.Monitors", "ScreenWidth");
            DropColumn("dbo.Monitors", "ScreenDiagonal");
            DropColumn("dbo.Monitors", "DisplaySpecId");
            DropTable("dbo.ThreeDs");
            DropTable("dbo.Speakers");
            DropTable("dbo.PanelColors");
            DropTable("dbo.Features");
            DropTable("dbo.CertificatesStandartsLicenses");
            DropTable("dbo.Cameras");
        }
    }
}
