using System;
using System.Data.Entity;


namespace Scheduler.Common.DataAccess
{
    /// <summary>
    /// The main DbContext of the application
    /// </summary>
    public class SchedulerContext : DbContext
    {

        public SchedulerContext()
            : base("SchedulerConnection")
        {
        }
        public DbSet<ClientDevice> Clients { get; set; }
        public DbSet<MachineCommand> Commands { get; set; }
        public DbSet<CommandExecution> CommandsExecutuions { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof (SchedulerContext).Assembly);
        }
    }
}