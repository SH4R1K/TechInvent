using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class RamDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("speed")]
        public int Speed { get; set; }

        [JsonPropertyName("capacity")]
        public string Capacity { get; set; }

        [JsonPropertyName("memory_type")]
        public int MemoryType { get; set; }

        [JsonPropertyName("part_number")]
        public string? PartNumber { get; set; }

        [JsonPropertyName("serial_number")]
        public string? SerialNumber { get; set; }
    }


}
