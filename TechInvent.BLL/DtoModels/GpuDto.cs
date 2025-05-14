
using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GpuDto
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("adapter_ram")]
        public double? AdapterRam { get; set; }

        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }
    }
}
