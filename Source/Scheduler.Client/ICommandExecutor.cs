﻿using System;
using System.Threading.Tasks;
using Scheduler.Common;

namespace Scheduler.Client
{
    /// <summary>
    /// Helper interface that simulates a command execution on a client machine and produces random success or error results
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes the supplied command and returns the updated version of it.
        /// </summary>
        Task<CommandExecution> ExecuteCommandAsync(CommandExecution commandExecution);
    }
}