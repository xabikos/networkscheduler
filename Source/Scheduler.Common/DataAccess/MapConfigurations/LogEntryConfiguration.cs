using System;
using System.Data.Entity.ModelConfiguration;
using Scheduler.Common.Logging;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="ExecutionLogEntry"/>
    /// </summary>
    internal class LogEntryConfiguration : EntityTypeConfiguration<LogEntry>
    {
        public LogEntryConfiguration()
        {
            ToTable("LogEntries");
        }
    }
}