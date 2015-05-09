using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Scheduler.Common;

namespace Scheduler.Server.Services
{
    public interface IConnectedClientsRegistry
    {

        IEnumerable<ConnectedClient> GetConnectedClients();

        bool RegisterClient(ClientDevice clientDevice, string connectionId);

        bool RemoveClient(string connectionId);

    }

    public class ConnectedClientsRegistry : IConnectedClientsRegistry
    {
        private static readonly ConcurrentDictionary<string, ConnectedClient> Connections =
            new ConcurrentDictionary<string, ConnectedClient>();

        public IEnumerable<ConnectedClient> GetConnectedClients()
        {
            return Connections.Values;
        }

        public bool RegisterClient(ClientDevice clientDevice, string connectionId)
        {
            var connectedClient = new ConnectedClient
            {
                ConnectionId = connectionId,
                ConnectedOn = DateTime.UtcNow,
                Client = clientDevice
            };
            return Connections.TryAdd(connectionId, connectedClient);
        }

        public bool RemoveClient(string connectionId)
        {
            ConnectedClient connectedClient;
            return Connections.TryRemove(connectionId, out connectedClient);
        }
    }
}