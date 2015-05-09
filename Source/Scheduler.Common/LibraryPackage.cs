using System;
using Scheduler.Common.DataAccess;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Scheduler.Common
{
    public class LibraryPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            // This instance is configured as Transient because we have not implemented
            // Unit of Work pattern so every time we use an instance we should call Save changes
            container.Register<SchedulerContext>(Lifestyle.Transient);
        }
    }
}
