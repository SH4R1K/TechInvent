using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GpuDto
    {
        public int IdGpu { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("adapter_ram")]
        public double? AdapterRam { get; set; }

        [JsonProperty("uuid")]
        public string? Uuid { get; set; }
    }
}
