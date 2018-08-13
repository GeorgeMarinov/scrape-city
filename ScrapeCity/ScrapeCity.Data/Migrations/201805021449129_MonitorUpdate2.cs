namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonitorUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Monitors", "HasCurvedPanel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Speakers", "HasSpeakers", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Speakers", "HasSpeakers");
            DropColumn("dbo.Monitors", "HasCurvedPanel");
        }
    }
}
