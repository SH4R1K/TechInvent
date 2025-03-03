using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class ProcessorDto : BaseDto
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
