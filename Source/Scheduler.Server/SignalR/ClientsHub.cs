using System;
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
        private readonly Lazy<IClientsManager> _clientsManager =
            new Lazy<IClientsManager>(() => new ClientsManager());


        public void ExecuteCommand(int id, int commandId)
        {
            
        }

        public override Task OnConnected()
        {
            var clientName = Context.Headers["authToken"];
            var clientAddress = Context.Headers["ipAddress"];

            _clientsManager.Value.ClientConnected(new ClientDevice
            {
                Name = clientName,
                IpAddress = clientAddress
            }, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _clientsManager.Value.ClientDisconected(Context.ConnectionId);
            
            return base.OnDisconnected(stopCalled);

        }
    }
}