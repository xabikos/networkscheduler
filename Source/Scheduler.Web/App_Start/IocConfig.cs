using System;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Scheduler.Web
{
    public class IocConfig
    {
        public static void InitializeIocContainer()
        {
            // Create the container as usual.
            var container = new Container();
            
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            // This is an extension method from SimpleInjector.Packaging that will scan
            // all assemblies in the project for IPackage implementations and allow
            // them to register objects in the container
            container.RegisterPackages();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}