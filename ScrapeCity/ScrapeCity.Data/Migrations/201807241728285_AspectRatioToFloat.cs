namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AspectRatioToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Monitors", "AspectRatio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Monitors", "AspectRatio", c => c.Int(nullable: false));
        }
    }
}
