using System;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
using Scheduler.Server.Services;

namespace Scheduler.Server.SignalR
{
    /// <summary>
    /// SignalR hub used for bidirectional communication with clients
    /// </summary>
    public class ManagementHub : Hub
    {
        private static readonly Lazy<IConnectedClientsRegistry> ClientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());


        public void ClientAdded(ClientDevice client)
        {
            Clients.All.clientAdded(client);
        }


        public void ClientDisconnected(string clientName)
        {
            Clients.All.clientDisconected(clientName);
        }

    }
}