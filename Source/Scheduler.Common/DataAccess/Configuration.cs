using System;
using System.Data.Entity;

namespace Scheduler.Common.DataAccess
{
    public class EntityFrameworkConfiguration : DbConfiguration
    {
        public EntityFrameworkConfiguration()
        {
            SetDatabaseInitializer( new DropCreateDatabaseAlways<SchedulerContext>());
        }
    }
}