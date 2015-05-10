using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Scheduler.Common;

namespace Scheduler.Client
{
    class Program
    {
        private static IHubProxy _clientsHub;

        static void Main()
        {            
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
        private static void SimulateCommandExecution(CommandExecution commandExecution)
        {
            commandExecution.StartExecution = DateTime.UtcNow;

            var rnd = new Random();
            commandExecution.Result = (ExecutionResult) rnd.Next(0, 2);
            Task.Delay(rnd.Next(1000, 5000)).Wait();
            
            commandExecution.FinishExecution = DateTime.UtcNow;
            _clientsHub.Invoke<CommandExecution>("ReportCommandResult", commandExecution).ContinueWith(task =>
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