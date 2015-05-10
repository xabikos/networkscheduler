using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
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


        public void ExecuteCommand(int id, int commandId)
        {
            // The client to execute command on is not connected any more so report to the caller that the command didn't executed
            if (ConnectedClientsRegistry.Value.GetConnectedClients().All(cc => cc.Client.Id != id))
            {
                Clients.Caller.commandExecutionFailed("Client is not reachable at this time");
            }
            //var clientMachineConnectionId = ClientsManager.Value.
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