using AutoMapper;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Brands.ViewModels;
using ScrapeCity.Common.Models.ImageAddresses;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ScrapeCity.PublicWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                #region Monitor > MonitorVm
                expression.CreateMap<VideoPort, VideoPortVm>()
                .ForMember(Vm =>
                Vm.Type, config => config
                .MapFrom(Model => Model.Type
                .ToString()));

                expression.CreateMap<USBPort, USBPortVm>()
                .ForMember(Vm =>
                Vm.Type, config => config
                .MapFrom(Model => Model.Type
                .ToString()));

                expression.CreateMap<MonitorImageAddress, MonitorPicturesVm>()
                .ForMember(Vm =>
                Vm.Uri, config => config
                .MapFrom(Model => Model.Uri));

                expression.CreateMap<Speakers, SpeakersVm>();
                expression.CreateMap<Camera, CameraVm>();

                expression.CreateMap<Monitor, MonitorVm>()
                .ForMember(Vm =>
                Vm.BrandId, config =>
                config
                .MapFrom(M => M.Brand.Id))

                .ForMember(Vm =>
                Vm.BrandName, config =>
                config
                .MapFrom(M => M.Brand.BrandName))

                .ForMember(Vm =>
                Vm.BacklightType, config => config
                 .MapFrom(Model => Model.BacklightType
                 .ToString()))

                .ForMember(Vm =>
                Vm.ScreenType, config => config
                 .MapFrom(Model => Model.ScreenType
                 .ToString()))

                 .ForMember(Vm =>
                Vm.PanelType, config => config
                 .MapFrom(Model => Model.PanelType
                 .ToString()))

                .ForMember(Vm =>
                Vm.DisplaySyncType, config => config
                 .MapFrom(Model => Model.DisplaySyncType
                 .ToString()))

                .ForMember(Vm => Vm.PanelColors, config =>
                   config.ResolveUsing((monitor, monitorvm, result) =>
                   {
                       var list = new List<string>();
                       foreach (var panelColor in monitor.PanelColors)
                       {
                           list.Add(panelColor.Value);
                       }
                       return list;
                   }))

                .ForMember(Vm => Vm.Features, config =>
                   config.ResolveUsing((monitor, monitorvm, result) =>
                   {
                       var list = new List<string>();
                       foreach (var feature in monitor.Features)
                       {
                           list.Add(feature.Value);
                       }
                       return list;
                   }))

                .ForMember(Vm => Vm.CertificatesStandartsLicenses, config =>
                   config.ResolveUsing((monitor, monitorvm, result) =>
                   {
                       var list = new List<string>();
                       foreach (var csl in monitor.CertificatesStandartsLicenses)
                       {
                           list.Add(csl.Value);
                       }
                       return list;
                   }))

                   .ForMember(Vm => Vm.UnhandledDisplaySpecificationProperties, config =>
                   config.ResolveUsing((monitor, monitorvm, result) =>
                   {
                       var list = new List<string>();
                       foreach (var unhandled in monitor.UnhandledDisplaySpecificationProperties)
                       {
                           list.Add(unhandled.Value);
                       }
                       return list;
                   }));
                    #endregion

                expression.CreateMap<Brand, BrandVm>();
            });
        }
    }
}
