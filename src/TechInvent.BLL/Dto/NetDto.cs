using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class NetDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("mac_address")]
        public string MacAddress { get; set; }

        [JsonPropertyName("adapter_type")]
        public string AdapterType { get; set; }
    }


}
