namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonitorUpdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CertificatesStandartsLicenses", newName: "CertificateStandartLicenses");
            RenameTable(name: "dbo.ThreeDs", newName: "UnhandledDIsplaySpecificationProperties");
            AddColumn("dbo.Monitors", "CIE1976", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "CIE1931", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "REC709", c => c.Double(nullable: false));
            AddColumn("dbo.Monitors", "ThreeD", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Monitors", "AspectRatio", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "NTSC", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "sRGB", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "AdobeRGB", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "DCI_P3", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "Rec2020", c => c.Double(nullable: false));
            AlterColumn("dbo.Cameras", "ImageResolutionPixels", c => c.String());
            AlterColumn("dbo.Cameras", "VideoResolutionPixels", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cameras", "VideoResolutionPixels", c => c.Int(nullable: false));
            AlterColumn("dbo.Cameras", "ImageResolutionPixels", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "Rec2020", c => c.String());
            AlterColumn("dbo.Monitors", "DCI_P3", c => c.String());
            AlterColumn("dbo.Monitors", "AdobeRGB", c => c.String());
            AlterColumn("dbo.Monitors", "sRGB", c => c.String());
            AlterColumn("dbo.Monitors", "NTSC", c => c.String());
            AlterColumn("dbo.Monitors", "AspectRatio", c => c.String());
            DropColumn("dbo.Monitors", "ThreeD");
            DropColumn("dbo.Monitors", "REC709");
            DropColumn("dbo.Monitors", "CIE1931");
            DropColumn("dbo.Monitors", "CIE1976");
            RenameTable(name: "dbo.UnhandledDIsplaySpecificationProperties", newName: "ThreeDs");
            RenameTable(name: "dbo.CertificateStandartLicenses", newName: "CertificatesStandartsLicenses");
        }
    }
}
