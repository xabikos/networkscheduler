using System;
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
}