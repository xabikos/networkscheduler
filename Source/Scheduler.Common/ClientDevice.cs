using System;
using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the device
        /// </summary>
        [JsonProperty("name")] 
        public string Name { get; set; }

        /// <summary>
        /// The IP address of the device
        /// </summary>
        [JsonProperty("ipAddress")] 
        public string IpAddress { get; set; }
    }
}
