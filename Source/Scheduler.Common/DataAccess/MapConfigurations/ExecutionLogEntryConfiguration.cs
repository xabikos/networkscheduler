using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Scheduler.Common.Logging;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="ExecutionLogEntry"/>
    /// </summary>
    internal class ExecutionLogEntryConfiguration : EntityTypeConfiguration<ExecutionLogEntry>
    {
        public ExecutionLogEntryConfiguration()
        {
            ToTable("ExecutionLogEntries");

            HasRequired(ele => ele.Execution).WithMany().HasForeignKey(ele => ele.ExecutionId).WillCascadeOnDelete(true);
        }
    }
}