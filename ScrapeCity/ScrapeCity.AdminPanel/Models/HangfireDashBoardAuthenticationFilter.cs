using Hangfire.Dashboard;
using Microsoft.Owin;
using System.Web;

namespace ScrapeCity.AdminPanel.Models
{
    public class HangfireDashBoardAuthenticationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            //return owinContext.Authentication.User.Identity.IsAuthenticated;

            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}