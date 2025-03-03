using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInventAPI.DtoModels
{
    public class DiskDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}
