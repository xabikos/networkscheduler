using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SimpleInjector;

namespace Scheduler.Server.SignalR
{
    /// <summary>
    /// Custom implementation of <see cref="DefaultDependencyResolver"/> in order to achieve IoC with SignalR and Simple Injector
    /// </summary>
    internal class SignalRSimpleInjectorDependencyResolver : DefaultDependencyResolver
    {
        private readonly Container _container;
        public SignalRSimpleInjectorDependencyResolver(Container container)
        {
            _container = container;
        }
        public override object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType)
                .Concat(base.GetServices(serviceType));
        }

    }
}
