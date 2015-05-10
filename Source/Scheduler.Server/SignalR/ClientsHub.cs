using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
using Scheduler.Common.DataAccess;
using Scheduler.Server.Services;

namespace Scheduler.Server.SignalR
{
    /// <summary>
    /// SignalR hub used for bidirectional communication with clients
    /// </summary>
    public class ClientsHub : Hub
    {
        private static readonly Lazy<IClientsManager> ClientsManager =
            new Lazy<IClientsManager>(() => new ClientsManager());
        private static readonly Lazy<IConnectedClientsRegistry> ConnectedClientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());

        /// <summary>
        /// This is called by all web app clients to be able to broadcast easier at a later time
        /// </summary>
        public async Task JoinWebApp()
        {
            await Groups.Add(Context.ConnectionId, Resources.WepAppClientsGroupName);
        }

        public void ExecuteCommand(int id, int commandId)
        {
            // The client to execute command on is not connected any more so report to the caller that the command didn't executed
            var connectedClient =
                ConnectedClientsRegistry.Value.GetConnectedClients().FirstOrDefault(cc => cc.Client.Id == id);
            if (connectedClient == null)
            {
                Clients.Caller.commandExecutionInfo("failed", "Client is not reachable at this time");
                return;
            }

            // Get the connectionId for the specific client device
            var connectionId = connectedClient.ConnectionId;
            using (var context = new SchedulerContext())
            {
                var command = context.Commands.First(c => c.Id == commandId);
                // Store in the database info about the current execution
                var commandExecution = new CommandExecution
                {
                    ClientId = id,
                    Command = command,
                    Type = MachineCommandType.ManualTriggered,
                    Result = ExecutionResult.Pending
                };
                context.CommandsExecutuions.Add(commandExecution);
                context.SaveChanges();
                Clients.Client(connectionId).executeCommand(commandExecution);
                Clients.Group(Resources.WepAppClientsGroupName).commandExecutionInfo("started", commandExecution);
            }
        }

        public async Task ReportCommandResult(CommandExecution commandExecution)
        {
            // Update the data for the supplied execution
            using (var context = new SchedulerContext())
            {
                var executionToUpdate = context.CommandsExecutuions.First(ce=>ce.Id == commandExecution.Id);
                executionToUpdate.Result = commandExecution.Result;
                executionToUpdate.StartExecution = commandExecution.StartExecution.HasValue
                    ? commandExecution.StartExecution.Value
                    : default(DateTime);
                executionToUpdate.FinishExecution = commandExecution.FinishExecution.HasValue
                    ? commandExecution.FinishExecution.Value
                    : default(DateTime);
                await context.SaveChangesAsync().ConfigureAwait(false);
                Clients.Group(Resources.WepAppClientsGroupName).commandExecutionInfo("finished", commandExecution);
            }
        }

        public override Task OnConnected()
        {
            var clientName = Context.Headers["authToken"];
            var clientAddress = Context.Headers["ipAddress"];
            
            // Only report the physical devices and not the web application connected to this hub
            if (!string.IsNullOrEmpty(clientName) && !string.IsNullOrEmpty(clientAddress))
            {
                ClientsManager.Value.ClientConnected(new ClientDevice
                {
                    Name = clientName,
                    IpAddress = clientAddress
                }, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ClientsManager.Value.ClientDisconected(Context.ConnectionId);
            
            return base.OnDisconnected(stopCalled);

        }
    }
}