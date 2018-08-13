namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatingPointNumberForAmperage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Monitors", "Amperage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Monitors", "Amperage", c => c.Int(nullable: false));
        }
    }
}
