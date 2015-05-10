using System;

namespace Scheduler.Common.Logging
{
    /// <summary>
    /// Command execution specific log entry
    /// </summary>
    public class ExecutionLogEntry : LogEntry
    {
        /// <summary>
        /// The id of the Execution
        /// </summary>
        public int ExecutionId { get; set; }
        
        /// <summary>
        /// The command execution the entry is about
        /// </summary>
        public virtual CommandExecution Execution { get; set; }

    }
}