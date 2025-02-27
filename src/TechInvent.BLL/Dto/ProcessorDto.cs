using Newtonsoft.Json;

namespace TechInvent.BLL.Dto
{
    public class ProcessorDto
    {
        public int IdProcessor { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("physical_cores")]
        public int PhysicalCores { get; set; }

        [JsonProperty("logical_cores")]
        public int LogicalCores { get; set; }

        [JsonProperty("max_clock_speed")]
        public int MaxClockSpeed { get; set; }
    }


}
