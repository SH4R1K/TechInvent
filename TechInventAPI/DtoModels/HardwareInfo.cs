using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class HardwareInfo
    {
        public int IdHardwareInfo { get; set; }
        [JsonProperty("mainboard")]
        public MainboardDto? Mainboard { get; set; }

        [JsonProperty("processor")]
        public List<ProcessorDto>? Processor { get; set; }

        [JsonProperty("ram")]
        public List<RamDto>? Ram { get; set; }

        [JsonProperty("net")]
        public List<NetDto>? Net { get; set; }

        [JsonProperty("gpu")]
        public List<GpuDto> Gpu { get; set; }

        [JsonProperty("perifery")]
        public Perifery? Perifery { get; set; }
        public int IdWorkplace { get; set; }
        public WorkplaceDto Workplace { get; set; }
    }
}
