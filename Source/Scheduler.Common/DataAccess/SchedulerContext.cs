using System;
using System.Data.Entity;
using Scheduler.Common.Logging;


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
        public DbSet<LogEntry> LogEntries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof (SchedulerContext).Assembly);
        }
    }
}