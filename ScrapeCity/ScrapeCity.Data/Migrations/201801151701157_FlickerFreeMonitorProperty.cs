namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlickerFreeMonitorProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monitors", "FlickerFree", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Monitors", "FlickerFree");
        }
    }
}
