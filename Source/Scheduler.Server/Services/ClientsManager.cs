using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
using Scheduler.Common.DataAccess;
using Scheduler.Server.SignalR;

namespace Scheduler.Server.Services
{
    internal class ClientsManager : IClientsManager
    {
        private readonly Lazy<IHubContext> _context =
            new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<ManagementHub>());
        private readonly Lazy<IConnectedClientsRegistry> _clientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());
        

        public bool ClientConnected(ClientDevice client, string connectionId)
        {
            using (var dbContext = new SchedulerContext())
            {
                if (!dbContext.Clients.Any(c=>c.Name == client.Name))
                {
                    dbContext.Clients.Add(client);
                    dbContext.SaveChanges();
                    _context.Value.Clients.All.clientAdded(client);
                }
                return _clientsRegistry.Value.RegisterClient(client, connectionId);
            }
        }

        public bool ClientDisconected(string connectionId)
        {
            var clientName =
                _clientsRegistry.Value.GetConnectedClients().First(c => c.ConnectionId == connectionId).Client.Name;
            _context.Value.Clients.All.clientDisconected(clientName);

            return _clientsRegistry.Value.RemoveClient(connectionId);
        }

    }
}