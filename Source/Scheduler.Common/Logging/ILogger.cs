using System;
using System.Threading.Tasks;

namespace Scheduler.Common.Logging
{
    /// <summary>
    /// Interface that all Loggers should implement in order to have a unified behavior of logging.
    /// </summary>
    public interface ILogger
    {
        Task Log(LogEntry logEntry);
    }
}
