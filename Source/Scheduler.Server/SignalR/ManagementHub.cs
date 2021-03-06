﻿using System;
using System.Linq;
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

        private readonly Lazy<IConnectedClientsRegistry> _connectedClientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());
        
        public void GetConnectedClients()
        {
            var enumerable = _connectedClientsRegistry.Value.GetConnectedClients().Select(cc => cc.Client.Name);
            Clients.Caller.connectedClients(enumerable);
        }

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