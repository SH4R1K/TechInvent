using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInventAPI.DtoModels
{    
    public class SoftwareDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("vendor")]
        public string Vendor { get; set; }
    }
}
