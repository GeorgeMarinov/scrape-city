namespace ScrapeCity.Data.Migrations
{
    using ScrapeCity.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Web;

    public partial class RenameMonitors : DbMigration
    {
        public override void Up()
        {
            var monitorUploadPath = Settings.ImagesServerUploadPath + "Monitor\\";
            using (var context = new ScrapeCityDbContext())
            {
                //this is not selecting the whole model because of forward compatability.
                // Ex. : if you add a new property and select the whole obj. when you have to run more than 1 migration
                // this one will fail because it cannot select the new properties as they're not in the DB
                var monitors = context.Monitors.Select(x => new {
                    x.Id,
                    MonitorPictures = x.MonitorImages.ToList(),
                    x.Brand.BrandName,
                    x.Model,
                    x.Thumbnail
                }).ToList();

                foreach (var monitor in monitors)
                {
                    var images = monitor.MonitorPictures;
                    foreach (var image in images)
                    {
                        var oldFileRelativePath = image.Uri;
                        var oldFileName = oldFileRelativePath.Substring(oldFileRelativePath.LastIndexOf("/") + 1);
                        var fileName = Guid.NewGuid().ToString() + ".jpg";

                        string hdddir = "";
                        if (Path.IsPathRooted(monitorUploadPath))
                        {
                            hdddir = Path.Combine(monitorUploadPath, monitor.BrandName, monitor.Model);
                        }
                        else
                        {
                            hdddir = Path.Combine(HttpRuntime.AppDomainAppPath, monitorUploadPath, monitor.BrandName, monitor.Model);
                        }

                        var oldHddDir = Path.Combine(hdddir, oldFileName);
                        var newHddDir = Path.Combine(hdddir, fileName);


                        try
                        {
                            System.IO.File.Move(oldHddDir, newHddDir);
                        }
                        catch (Exception)
                        {
 
                        }
                        
                        
                        

                        var imageUrlPath = $@"/{monitor.BrandName}/{monitor.Model}/{fileName}";

                         
                        if (monitor.Thumbnail != "/placeholder.jpg")
                        {
                            if (monitor.Thumbnail == image.Uri)
                            {
                                // Since we can't get the whole model, we have to update just the thumbnail, manually.
                                context.Database.ExecuteSqlCommand("UPDATE dbo.Monitors SET Thumbnail = @Thumbnail WHERE Id = @Id", new SqlParameter("Thumbnail",imageUrlPath), new SqlParameter("Id", monitor.Id)); 
                            }
                        }

                        image.Uri = imageUrlPath;
                    }
                    context.SaveChanges();
                }
            }
        }
            
        public override void Down()
        {
        }
    }
}
