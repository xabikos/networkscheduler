using System;
using Scheduler.Common;

namespace Scheduler.Server.Services
{
    public interface IClientsManager
    {
        bool ClientConnected(ClientDevice client, string connectionId);

        bool ClientDisconected(string connectionId);
    }
}