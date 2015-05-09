using System;

namespace Scheduler.Common
{
    /// <summary>
    /// Hold the data for the currently connected clients to the server
    /// </summary>
    public class ConnectedClient
    {
        /// <summary>
        /// The id of the entity
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The client's Id
        /// </summary>
        public int ClientId { get; set; }
        
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