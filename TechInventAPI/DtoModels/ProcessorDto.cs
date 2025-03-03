using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInventAPI.DtoModels
{
    public class ProcessorDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("physical_cores")]
        public int PhysicalCores { get; set; }

        [JsonPropertyName("logical_cores")]
        public int LogicalCores { get; set; }

        [JsonPropertyName("max_clock_speed")]
        public int MaxClockSpeed { get; set; }
    }


}
