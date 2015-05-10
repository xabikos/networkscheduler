using System;

namespace Scheduler.Common
{
    /// <summary>
    /// Represents the execution of a command
    /// </summary>
    public class CommandExecution
    {
        /// <summary>
        /// The Id of the command execution
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Type of the command
        /// </summary>
        public MachineCommandType Type { get; set; }

        /// <summary>
        /// The result of the execution
        /// </summary>
        public ExecutionResult Result { get; set; }

        /// <summary>
        /// The id of the command that executed
        /// </summary>
        public int CommandId { get; set; }

        /// <summary>
        /// The command that executed
        /// </summary>
        public virtual MachineCommand Command { get; set; }

        /// <summary>
        /// The id of the client the command executed on
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// The client device the command executed on
        /// </summary>
        public virtual ClientDevice Client { get; set; }

        /// <summary>
        /// When the command started the execution
        /// </summary>
        public DateTime? StartExecution { get; set; }

        /// <summary>
        /// When the command finished the execution
        /// </summary>
        public DateTime? FinishExecution { get; set; }

    }
}