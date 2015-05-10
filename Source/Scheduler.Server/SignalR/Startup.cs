using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Owin;
using Scheduler.Server.Services;
using SimpleInjector;

namespace Scheduler.Server.SignalR
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            var container = new Container();

            container.Register<IConnectedClientsRegistry, ConnectedClientsRegistry>(Lifestyle.Singleton);
            container.Register<IClientsManager, ClientsManager>(Lifestyle.Transient);
            container.Register<ClientsHub>(() => new ClientsHub(container.GetInstance<IClientsManager>()));
            container.Register<IHubContext>(
                () => container.GetInstance<IConnectionManager>().GetHubContext<ManagementHub>());
            //container.Register<ManagementHub>(
            //    () => new ManagementHub(container.GetInstance<IConnectedClientsRegistry>()));
            
            // This is an extension method from SimpleInjector.Packaging that will scan
            // all assemblies in the project for IPackage implementations and allow
            // them to register objects in the container
            container.RegisterPackages();

            container.Verify();

            var config = new HubConfiguration
            {
                Resolver = new SignalRSimpleInjectorDependencyResolver(container)
            };
            //var activator = new SimpleInjectorHubActivator(container);
            //GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);
            
            app.MapSignalR(config);
        }
    }
}