namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SplitHorizontalFrequencyIntoMinAndMax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monitors", "MinHorizontalFrequency", c => c.Int(nullable: false));
            AddColumn("dbo.Monitors", "MaxHorizontalFrequency", c => c.Int(nullable: false));
            DropColumn("dbo.Monitors", "HorizontalFrequency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Monitors", "HorizontalFrequency", c => c.Int(nullable: false));
            DropColumn("dbo.Monitors", "MaxHorizontalFrequency");
            DropColumn("dbo.Monitors", "MinHorizontalFrequency");
        }
    }
}
