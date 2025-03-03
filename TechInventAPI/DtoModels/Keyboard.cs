using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInventAPI.DtoModels
{
    public class Keyboard
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }


}
