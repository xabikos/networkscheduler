using System;
using System.Collections.Generic;

namespace Scheduler.Common
{
    /// <summary>
    /// Represents command that should be executed in a remote machine
    /// </summary>
    public class MachineCommand
    {
        /// <summary>
        /// The id of the command
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the command
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Category of the command
        /// </summary>
        public MachineCommandCategory Category { get; set; }

    }
}