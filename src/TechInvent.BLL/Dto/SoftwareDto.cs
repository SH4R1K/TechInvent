using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class SoftwareDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("vendor")]
        public string Vendor { get; set; }
    }
}
