namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Monitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonitorURL = c.String(),
                        Model = c.String(),
                        UPC = c.String(),
                        Thumbnail = c.String(),
                        Brightness = c.Int(nullable: false),
                        ResponseTime = c.Int(nullable: false),
                        PPI = c.Int(nullable: false),
                        DisplaySyncType = c.Int(nullable: false),
                        HorizontalViewingAngle = c.Int(nullable: false),
                        VerticalViewingAngle = c.Int(nullable: false),
                        AspectRatio = c.String(),
                        ScreenSize = c.Double(nullable: false),
                        ColorDepth = c.Int(nullable: false),
                        MaxHorizontalPixels = c.Int(nullable: false),
                        MaxVerticalPixels = c.Int(nullable: false),
                        MaxFrequencyAtMaxResolution = c.Int(nullable: false),
                        CurvedPanel = c.Boolean(nullable: false),
                        PanelType = c.Int(nullable: false),
                        ScreenType = c.Int(nullable: false),
                        BacklightType = c.Int(nullable: false),
                        ContrastRatio = c.String(),
                        DynamicContrastRatio = c.Long(nullable: false),
                        PowerConsumptionMax = c.Double(nullable: false),
                        PowerConsumptionMin = c.Double(nullable: false),
                        VesaMount = c.Boolean(nullable: false),
                        VesaHoleDistance = c.Int(nullable: false),
                        AdobeRgb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sRGB = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MTBF = c.Int(nullable: false),
                        TouchScreen = c.Boolean(nullable: false),
                        LowBlueLight = c.Boolean(nullable: false),
                        HDCP_Support = c.Boolean(nullable: false),
                        PictureByPicture = c.Boolean(nullable: false),
                        PictureInPicture = c.Boolean(nullable: false),
                        CardReader = c.Boolean(nullable: false),
                        MHL = c.Boolean(nullable: false),
                        KensingtonLock = c.Boolean(nullable: false),
                        MacCompatable = c.Boolean(nullable: false),
                        KVM_Switch = c.Boolean(nullable: false),
                        AudioIn = c.Boolean(nullable: false),
                        AudioOut = c.Boolean(nullable: false),
                        Tilt = c.Boolean(nullable: false),
                        Swivel = c.Boolean(nullable: false),
                        Pivot = c.Boolean(nullable: false),
                        HeightAdjustable = c.Boolean(nullable: false),
                        Speakers = c.Boolean(nullable: false),
                        AutoBrightness = c.Boolean(nullable: false),
                        EnergyStar = c.Boolean(nullable: false),
                        Brand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Brand_Id);
            
            CreateTable(
                "dbo.MonitorImageAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.USBPorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Num = c.Int(nullable: false),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            CreateTable(
                "dbo.VideoPorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Num = c.Int(nullable: false),
                        MonitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Monitors", t => t.MonitorId, cascadeDelete: true)
                .Index(t => t.MonitorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MonitorImageAddressMonitors",
                c => new
                    {
                        MonitorImageAddress_Id = c.Int(nullable: false),
                        Monitor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MonitorImageAddress_Id, t.Monitor_Id })
                .ForeignKey("dbo.MonitorImageAddresses", t => t.MonitorImageAddress_Id, cascadeDelete: true)
                .ForeignKey("dbo.Monitors", t => t.Monitor_Id, cascadeDelete: true)
                .Index(t => t.MonitorImageAddress_Id)
                .Index(t => t.Monitor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.VideoPorts", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.USBPorts", "MonitorId", "dbo.Monitors");
            DropForeignKey("dbo.MonitorImageAddressMonitors", "Monitor_Id", "dbo.Monitors");
            DropForeignKey("dbo.MonitorImageAddressMonitors", "MonitorImageAddress_Id", "dbo.MonitorImageAddresses");
            DropForeignKey("dbo.Monitors", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.MonitorImageAddressMonitors", new[] { "Monitor_Id" });
            DropIndex("dbo.MonitorImageAddressMonitors", new[] { "MonitorImageAddress_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.VideoPorts", new[] { "MonitorId" });
            DropIndex("dbo.USBPorts", new[] { "MonitorId" });
            DropIndex("dbo.Monitors", new[] { "Brand_Id" });
            DropTable("dbo.MonitorImageAddressMonitors");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VideoPorts");
            DropTable("dbo.USBPorts");
            DropTable("dbo.MonitorImageAddresses");
            DropTable("dbo.Monitors");
            DropTable("dbo.Brands");
        }
    }
}
