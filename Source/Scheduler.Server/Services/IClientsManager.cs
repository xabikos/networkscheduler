using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
using Scheduler.Common.DataAccess;
using Scheduler.Server.SignalR;

namespace Scheduler.Server.Services
{
    public interface IClientsManager
    {
        bool ClientConnected(ClientDevice client, string connectionId);

        bool ClientDisconected(string connectionId);
    }

    internal class ClientsManager : IClientsManager
    {
        private readonly Lazy<IHubContext> _context =
            new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<ManagementHub>());

        private static readonly Lazy<IConnectedClientsRegistry> ClientsRegistry =
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
                return ClientsRegistry.Value.RegisterClient(client, connectionId);
            }
        }

        public bool ClientDisconected(string connectionId)
        {
            var clientName =
                ClientsRegistry.Value.GetConnectedClients().First(c => c.ConnectionId == connectionId).Client.Name;
            _context.Value.Clients.All.clientDisconected(clientName);

            return ClientsRegistry.Value.RemoveClient(connectionId);
        }

    }
}