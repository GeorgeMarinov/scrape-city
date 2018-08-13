using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScrapeCity.PublicWebsite.Startup))]
namespace ScrapeCity.PublicWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
