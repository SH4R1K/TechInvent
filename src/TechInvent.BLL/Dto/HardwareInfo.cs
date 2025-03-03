using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class HardwareInfo
    {
        [JsonPropertyName("mainboard")]
        public MainboardDto? Mainboard { get; set; }

        [JsonPropertyName("processor")]
        public List<ProcessorDto>? Processor { get; set; }

        [JsonPropertyName("ram")]
        public List<RamDto>? Ram { get; set; }

        [JsonPropertyName("net")]
        public List<NetDto>? Net { get; set; }

        [JsonPropertyName("gpu")]
        public List<GpuDto> Gpu { get; set; }
        
        [JsonPropertyName("disk")]
        public List<DiskDto> Disks { get; set; }
    }
}
