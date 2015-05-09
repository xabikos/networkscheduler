using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Scheduler.Server.SignalR
{
    /// <summary>
    /// SignalR hub used for bidirectional communication with clients
    /// </summary>
    public class ClientsHub : Hub
    {
        public override Task OnConnected()
        {
            var clientName = Context.Headers["authToken"];

            return base.OnConnected();
        }
    }
}