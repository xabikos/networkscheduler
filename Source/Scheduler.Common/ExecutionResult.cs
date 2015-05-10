using System;

namespace Scheduler.Common
{
    /// <summary>
    /// Indicates the result of command's execution
    /// </summary>
    public enum ExecutionResult
    {
        Successful,
        Pending,
        Faulted
    }
}