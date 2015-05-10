using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="CommandExecution"/>
    /// </summary>
    internal class CommandExecutionConfiguration : EntityTypeConfiguration<CommandExecution>
    {
        public CommandExecutionConfiguration()
        {
            ToTable("CommandsExecution");
            
            Property(ce => ce.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(ce => ce.Client)
                .WithMany(cd => cd.Executions)
                .HasForeignKey(ce => ce.ClientId)
                .WillCascadeOnDelete(true);
            HasRequired(ce => ce.Command)
                .WithMany(c => c.Executions)
                .HasForeignKey(ce => ce.CommandId)
                .WillCascadeOnDelete(false);

        }
    }
}