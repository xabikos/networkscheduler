using System;

namespace Scheduler.Common
{
    /// <summary>
    /// Represents the various client devices
    /// </summary>
    public class ClientDevice
    {
        /// <summary>
        /// The id of the entity
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the device
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The IP address of the device
        /// </summary>
        public string IpAddress { get; set; }
    }
}
