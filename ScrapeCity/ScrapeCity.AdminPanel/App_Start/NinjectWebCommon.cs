[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ScrapeCity.AdminPanel.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ScrapeCity.AdminPanel.App_Start.NinjectWebCommon), "Stop")]

namespace ScrapeCity.AdminPanel.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ScrapeCity.Domain.Interfaces;
    using ScrapeCity.Domain.MonitorServices;
    using ScrapeCity.Domain.BrandServices;
    using Ninject.Web.Common.WebHost;
    using Hangfire;
    using ScrapeCity.Data;
    using ScrapeCity.AdminPanel.HangfireJobs;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.UseNinjectActivator(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMonitorService>().To<MonitorService>();
            kernel.Bind<IBrandService>().To<BrandService>();
            kernel.Bind<ScrapeCityDbContext>().ToSelf().InBackgroundJobScope();
            kernel.Bind<IAlgoliaMonitorIndex>().To<AlgoliaMonitorIndex>();
            kernel.Bind<IDisplaySpecificationsScraper>().To<DisplaySpecificationsScraper>();
        }
    }
}
