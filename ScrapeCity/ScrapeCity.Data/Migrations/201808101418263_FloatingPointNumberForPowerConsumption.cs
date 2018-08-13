namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatingPointNumberForPowerConsumption : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Monitors", "PowerConsumptionOff", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionSleep", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionAverage", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionEco", c => c.Double(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionMaximum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Monitors", "PowerConsumptionMaximum", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionEco", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionAverage", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionSleep", c => c.Int(nullable: false));
            AlterColumn("dbo.Monitors", "PowerConsumptionOff", c => c.Int(nullable: false));
        }
    }
}
