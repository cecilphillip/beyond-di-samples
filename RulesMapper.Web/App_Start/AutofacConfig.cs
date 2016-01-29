using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using RulesMapper.Core.Configuration;
using RulesMapper.Web.Hubs;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace RulesMapper.Web.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();

            // Register all referenced Autofac modules
            //BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var assemblies = AppDomain.CurrentDomain
                                      .GetAssemblies()
                                      .Where(asm => asm.FullName.StartsWith("RulesMapper", StringComparison.OrdinalIgnoreCase))
                                      .ToArray();

            builder.RegisterAssemblyModules(assemblies);


            // Register Hubs as Keyed Services since HubContext is non-generic
            builder.Register(c => GlobalHost.ConnectionManager.GetHubContext<EventsHub>())
                .Keyed<IHubContext>("EventsHub");

            // Register MVC Controllers
            builder.RegisterControllers(currentAssembly);

            // Register Web API Controllers
            builder.RegisterApiControllers(currentAssembly);

            // Register SignalR Hubs
            builder.RegisterHubs(currentAssembly);

            builder.RegisterType<AppSettingsConfigurationSource>().As<IConfigurationSource>()
                    .SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            return container;
        }
    }
}