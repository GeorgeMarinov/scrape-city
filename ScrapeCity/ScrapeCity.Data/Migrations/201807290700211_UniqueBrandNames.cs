namespace ScrapeCity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueBrandNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "BrandName", c => c.String(maxLength: 450));
            CreateIndex("dbo.Brands", "BrandName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Brands", new[] { "BrandName" });
            AlterColumn("dbo.Brands", "BrandName", c => c.String());
        }
    }
}
