using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{    
    public class SoftwareDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }
    }
}
