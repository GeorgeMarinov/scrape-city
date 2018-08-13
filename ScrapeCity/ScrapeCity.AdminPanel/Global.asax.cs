using AutoMapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using ScrapeCity.Domain.Validation;
using ScrapeCity.Common.Enums;
using System;
using ScrapeCity.Common.Models.ImageAddresses;
using System.Web;
using ScrapeCity.AdminPanel.Controllers;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using ScrapeCity.Common.Models.Monitors;
using ScrapeCity.Common.Models.Monitors.MonitorProperties;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Brands.ViewModels;
using System.Collections.Generic;

namespace ScrapeCity.AdminPanel
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log
      = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new ValidatorFactory();
            });
        }

        protected void Application_Error()
        {
            //get error
            var ex = Server.GetLastError();
            //log error
            log.Error("Error",ex);

            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;
            //controller params
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "General");
            // Call target Controller
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                    new HttpContextWrapper(Context), routeData));
            // Clear the error from the server
            Server.ClearError();

            //just in case template
            //if (httpException == null)
            //{
            //    routeData.Values.Add("action", "Index");
            //}
            //else //It's an Http Exception, Let's handle it.
            //{
            //    switch (httpException.GetHttpCode())
            //    {
            //        case 404:
            //            // Page not found.
            //            routeData.Values.Add("action", "HttpError404");
            //            break;
            //        case 500:
            //            // Server error.
            //            routeData.Values.Add("action", "HttpError500");
            //            break;

            //        // Here you can handle Views to other error codes.
            //        // I choose a General error template  
            //        default:
            //            routeData.Values.Add("action", "General");
            //            break;
            //    }
            //}
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Monitor, MonitorListVm>();

                #region MonitorVm > Monitor
                expression.CreateMap<MonitorVm, Monitor>()
                    .ForMember(m => m.MonitorImages, config =>
                    config.Ignore())
                    .ForMember(m => m.Brand, config =>
                    config.Ignore())
                    .ForMember(m => m.BacklightType, config =>
                    config.ResolveUsing((monitorvm, monitor, result) =>
                    {
                        var enumVal = Backlight.NotAvailable;
                        Enum.TryParse(monitorvm.BacklightType, out enumVal);
                        return enumVal;
                    }))
                    .ForMember(m => m.ScreenType, config =>
                   config.ResolveUsing((monitorvm, monitor, result) =>
                   {
                       var enumVal = ScreenType.NotAvailable;
                       Enum.TryParse(monitorvm.ScreenType, out enumVal);
                       return enumVal;
                   }))
                    .ForMember(m => m.PanelType, config =>
                   config.ResolveUsing((monitorvm, monitor, result) =>
                   {
                       var enumVal = PanelType.NotAvailable;
                       Enum.TryParse(monitorvm.PanelType, out enumVal);
                       return enumVal;
                   }))
                     .ForMember(m => m.VideoPorts, config => config.Ignore())
                     .ForMember(m => m.USBPorts, config => config.Ignore())
                     .ForMember(m => m.Thumbnail, config => config.Ignore())
                     .ForMember(m => m.DisplaySyncType, config =>
                   config.ResolveUsing((monitorvm, monitor, result) =>
                   {
                       var enumVal = DisplaySyncType.None;
                       Enum.TryParse(monitorvm.DisplaySyncType, out enumVal);
                       return enumVal;
                   }))
                   .ForMember(m => m.PanelColors, config => config.Ignore())
                   .ForMember(m => m.CertificatesStandartsLicenses, config => config.Ignore())
                   .ForMember(m => m.Features, config => config.Ignore())
                .ForMember(m => m.UnhandledDisplaySpecificationProperties, config => config.Ignore());

                expression.CreateMap<CameraVm, Camera>();
                expression.CreateMap<SpeakersVm, Speakers>();
                #endregion

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


                expression.CreateMap<BrandVm, Brand>();
            });
        }
    }
}
