using System;

namespace Scheduler.Common.Logging
{
    /// <summary>
    /// Represents a Log entry
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The creation date of the log entry
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The level of the log
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// The message of the entry
        /// </summary>
        public string Message { get; set; }

    }
}