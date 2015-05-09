using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Scheduler.Common;
using Scheduler.Common.DataAccess;

namespace Scheduler.Server.SignalR
{
    /// <summary>
    /// SignalR hub used for bidirectional communication with clients
    /// </summary>
    public class ClientsHub : Hub
    {
        private static readonly Lazy<IConnectedClientsRegistry> ClientsRegistry =
            new Lazy<IConnectedClientsRegistry>(() => new ConnectedClientsRegistry());

        public override Task OnConnected()
        {
            var clientName = Context.Headers["authToken"];
            var clientAddress = Context.Headers["ipAddress"];

            using (var dbContext = new SchedulerContext())
            {
                var client = dbContext.Clients.FirstOrDefault(c => c.Name == clientName);
                if (client == null)
                {
                    client = new ClientDevice
                    {
                        Name = clientName,
                        IpAddress = clientAddress
                    };
                    dbContext.Clients.Add(client);
                    dbContext.SaveChanges();
                }
                ClientsRegistry.Value.RegisterClient(client, Context.ConnectionId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ClientsRegistry.Value.RemoveClient(Context.ConnectionId);
            
            return base.OnDisconnected(stopCalled);
        }
    }
}