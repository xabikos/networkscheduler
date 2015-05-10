using System;

namespace Scheduler.Common
{
    /// <summary>
    /// Hold the data for the currently connected clients to the server
    /// </summary>
    public class ConnectedClient
    {
        /// <summary>
        /// The connection Id between the server and the client
        /// </summary>
        public string ConnectionId { get; set; }
        
        /// <summary>
        /// The client
        /// </summary>
        public ClientDevice Client { get; set; }

        /// <summary>
        /// Indicates when the clients connected
        /// </summary>
        public DateTime ConnectedOn { get; set; }

    }
}