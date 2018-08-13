using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using ScrapeCity.AdminPanel.HangfireJobs;
using ScrapeCity.AdminPanel.Models;
using ScrapeCity.Common;
using System;
using System.IO;
using System.Web;

[assembly: OwinStartup(typeof(ScrapeCity.AdminPanel.Startup))]

namespace ScrapeCity.AdminPanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(Settings.HangfireConnectionString);

            var options = new DashboardOptions()
            {
                Authorization = new[]
                {
                    new HangfireDashBoardAuthenticationFilter()
                }
            };

            app.UseHangfireDashboard("/hangfire", options);
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                WorkerCount = Environment.ProcessorCount,
            });

            ConfigureJobs();


            if (Path.IsPathRooted(Settings.ImagesServerUploadPath))
            {
                Settings.imagesHDD_Path = Path.Combine(Settings.ImagesServerUploadPath);
            }
            else
            {
                Settings.imagesHDD_Path = Path.Combine(HttpRuntime.AppDomainAppPath, Settings.ImagesServerUploadPath);
            }
        }

        private void ConfigureJobs()
        {
            RecurringJob.AddOrUpdate<DisplaySpecificationsScraper>(x=>x.Scrape(), Cron.Daily);
        }
    }
}
