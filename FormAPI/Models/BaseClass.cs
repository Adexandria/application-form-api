using Newtonsoft.Json;

namespace FormAPI.Models
{
    /// <summary>
    /// Base class of all entities
    /// </summary>
    public  class BaseClass
    {
        /// <summary>
        /// A construtor to initialise id
        /// </summary>
        public BaseClass()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Id of the entity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
