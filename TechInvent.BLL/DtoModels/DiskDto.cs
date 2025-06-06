using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class DiskDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}
