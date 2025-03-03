using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class GpuDto : BaseDto
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("adapter_ram")]
        public double? AdapterRam { get; set; }

        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }
    }
}
