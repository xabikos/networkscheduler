using System;
using System.Threading.Tasks;
using Scheduler.Common.DataAccess;
using Scheduler.Common.Logging;

namespace Scheduler.Server.Services
{
    /// <summary>
    /// Custom implementation of <see cref="ILogger"/> for the management service
    /// </summary>
    internal class ServiceLogger : ILogger
    {
        public async Task Log(LogEntry logEntry)
        {
            using (var context = new SchedulerContext())
            {
                logEntry.CreatedAt = DateTime.UtcNow;
                context.LogEntries.Add(logEntry);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

    }
}