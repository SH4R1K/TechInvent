
using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class Printer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }


}
