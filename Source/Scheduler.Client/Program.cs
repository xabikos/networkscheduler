using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.AspNet.SignalR.Client;
using Scheduler.Common;
using Scheduler.Common.Logging;

namespace Scheduler.Client
{
    class Program
    {
        private static IHubProxy _clientsHub;
        private static ICommandExecutor _commandExecutor;

        static void Main()
        {
            // HACK As we don't have IoC here we create the instance ourself
            _commandExecutor = new CommandExecutor();
            //Set connection
            var connection = new HubConnection("http://localhost:8080/");
            //Make proxy to hub based on hub name on server
            _clientsHub = connection.CreateHubProxy("ClientsHub");
            //connection.Headers.Add("authToken", Environment.MachineName);
            var randomName = Guid.NewGuid().ToString();
            connection.Headers.Add("authToken", randomName);
            Console.WriteLine("I am the client machine with name: {0}", randomName);
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                connection.Headers.Add("ipAddress",
                    host.AddressList.First(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString());
            }
            
            //Start connection
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();
            
            _clientsHub.On<CommandExecution>("executeCommand", SimulateCommandExecution);

            Console.Read();
            connection.Stop();
        }

        /// <summary>
        /// Helper method that simulates a command execution on a client machine and produces random success or error results
        /// In general async void methods is not a good idea but here is used like event handler
        /// </summary>
        private static async void SimulateCommandExecution(CommandExecution commandExecution)
        {
            Log(LogLevel.Verbose, commandExecution.Id,
                string.Format("Start Executing Command {0} on client with name: {1} at {2}",
                    commandExecution.Command.Name, Environment.MachineName, DateTime.UtcNow));

            try
            {
                commandExecution = await _commandExecutor.ExecuteCommandAsync(commandExecution);
                
                await _clientsHub.Invoke<CommandExecution>("ReportCommandResult", commandExecution).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to update server for command execution:");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully update server for command execution:");
                    }
                    PrintCommandExecutionDetails(commandExecution);
                    Console.ResetColor();
                });
                
                Log(LogLevel.Information, commandExecution.Id,
                     string.Format("Finished Executing Command {0} on client with name: {1} at {2}",
                    commandExecution.Command.Name, Environment.MachineName, DateTime.UtcNow));
            }
            catch (InvalidOperationException ex)
            {
                Log(LogLevel.Error, commandExecution.Id,
                    string.Format("Exception with message {0} raised during executing command with name: {1}",
                        ex.Message, commandExecution.Command.Name));
            }
        }
        
        private static void Log(LogLevel level, int executionId, string message)
        {
            _clientsHub.Invoke<ExecutionLogEntry>("Log",
                new ExecutionLogEntry {Level = level, ExecutionId = executionId, Message = message});
        }

        private static void PrintCommandExecutionDetails(CommandExecution commandExecution)
        {
            Console.WriteLine("************************************");
            Console.WriteLine("Command Execution Id: {0}", commandExecution.Id);
            Console.WriteLine("Command Execution Result: {0}", commandExecution.Result);
            Console.WriteLine("Command Execution Type: {0}", commandExecution.Type);
            Console.WriteLine("Command Duration: {0}", commandExecution.FinishExecution.Value.Subtract(commandExecution.StartExecution.Value));
            Console.WriteLine("Command Name: {0}", commandExecution.Command.Name);
            Console.WriteLine("Command Category: {0}", commandExecution.Command.Category);
            Console.WriteLine("************************************");
        }

    }
}