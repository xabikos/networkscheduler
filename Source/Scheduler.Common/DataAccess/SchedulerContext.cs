using System;
using System.Data.Entity;

namespace Scheduler.Common.DataAccess
{
    public class SchedulerContext : DbContext
    {

        public SchedulerContext()
            : base("SchedulerConnection")
        {
        }

        public DbSet<ConnectedClient> ConnectedClients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof (SchedulerContext).Assembly);
        }
    }
}