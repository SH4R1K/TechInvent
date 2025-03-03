using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInventAPI.DtoModels
{
    public class MainboardDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("serial_number")]
        public string SerialNumber { get; set; }
    }


}
