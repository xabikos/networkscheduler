using System;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Common;

namespace Scheduler.Client
{
    public class CommandExecutor : ICommandExecutor
    {

        public async Task<CommandExecution> ExecuteCommandAsync(CommandExecution commandExecution)
        {
            commandExecution.StartExecution = DateTime.UtcNow;

            var rnd = new Random();
            commandExecution.Result = (ExecutionResult)rnd.Next(0, 2);
            // Virtual command execution time
            await Task.Delay(rnd.Next(1000, 5000));

            if (ShouldSimulateException())
            {
                throw new InvalidOperationException("An exception raised during the command execution");
            }

            commandExecution.FinishExecution = DateTime.UtcNow;
            return commandExecution;
        }

        private static bool ShouldSimulateException()
        {
            var secondsToSimulateException = new[] { 2, 5, 7 };
            var value = DateTime.UtcNow.Second % 10;
            return secondsToSimulateException.Contains(value);
        }

    }
}