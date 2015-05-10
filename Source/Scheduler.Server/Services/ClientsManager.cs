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
        private static readonly Lazy<IHubContext> Context =
            new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<ManagementHub>());
        private static readonly Lazy<IConnectedClientsRegistry> ClientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());


        public bool ClientConnected(ClientDevice client, string connectionId)
        {
            using (var dbContext = new SchedulerContext())
            {
                if (!dbContext.Clients.Any(c => c.Name == client.Name))
                {
                    dbContext.Clients.Add(client);
                    dbContext.SaveChanges();
                    Context.Value.Clients.All.clientAdded(client);
                }
                return ClientsRegistry.Value.RegisterClient(client, connectionId);
            }
        }

        public bool ClientDisconected(string connectionId)
        {
            // Verify that the disconnected client is network machine and not a web application client
            var deviceClient = ClientsRegistry.Value.GetConnectedClients().FirstOrDefault(c => c.ConnectionId == connectionId);
            if (deviceClient != null)
            {
                var clientName =
                    ClientsRegistry.Value.GetConnectedClients().First(c => c.ConnectionId == connectionId).Client.Name;
                Context.Value.Clients.All.clientDisconected(clientName);

                return ClientsRegistry.Value.RemoveClient(connectionId);
            }
            return true;
        }

    }
}