using System;

namespace Scheduler.Server
{
    /// <summary>
    /// DTO class representing command that should be executed in a remote machine
    /// </summary>
    public class MachineCommand
    {
        /// <summary>
        /// The name of the command
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the command
        /// </summary>
        public MachineCommandType Type { get; set; }

    }
}