using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using RulesMapper.Web.App_Start;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(Bootstrapper), "PreStart")]
[assembly: PostApplicationStartMethod(typeof(Bootstrapper), "PostStart")]
[assembly: ApplicationShutdownMethod(typeof(Bootstrapper), "Stop")]
[assembly: OwinStartup(typeof(Bootstrapper))]

namespace RulesMapper.Web.App_Start
{

    public class Bootstrapper
    {
        internal static IContainer Container;

        public static void PreStart()
        {
            Container = AutofacConfig.RegisterServices();

            //view lookup optimization. only interested in Razor
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureWebAPI(app);
            ConfigureSignalR(app);
        }

        private void ConfigureWebAPI(IAppBuilder app)
        {            
            var config = new HttpConfiguration();

            // Only support JSON formats
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Map routes
            config.MapHttpAttributeRoutes();

            // Plugin Autofac
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            // Add to OWIN pipeline
            app.UseWebApi(config);
        }

        private void ConfigureSignalR(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
#if DEBUG
                EnableDetailedErrors = true
#endif
            };
            app.MapSignalR(hubConfiguration);          
        }

        public static void PostStart()
        {           
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static void Stop()
        {
            // Stopping...
        }
    }
}