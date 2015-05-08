using System;

namespace Scheduler.Server
{
    /// <summary>
    /// The various types of the commands
    /// </summary>
    public enum MachineCommandType
    {
        UpdateDotNetFramework,
        UpdateWindows,
        CleanFileDisk
    }
}