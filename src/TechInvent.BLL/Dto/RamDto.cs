using Newtonsoft.Json;

namespace TechInvent.BLL.Dto
{
    public class RamDto
    {
        public int IdRam { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("memory_type")]
        public int MemoryType { get; set; }

        [JsonProperty("part_number")]
        public string? PartNumber { get; set; }

        [JsonProperty("serial_number")]
        public string? SerialNumber { get; set; }
    }


}
