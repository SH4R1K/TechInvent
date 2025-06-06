
using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class HardwareInfo
    {
        [JsonPropertyName("mainboard")]
        public MainboardDto? Mainboard { get; set; }

        [JsonPropertyName("processor")]
        public List<ProcessorDto>? Processor { get; set; } = new List<ProcessorDto>();

        [JsonPropertyName("ram")]
        public List<RamDto>? Ram { get; set; } = new List<RamDto>();

        [JsonPropertyName("net")]
        public List<NetDto>? Net { get; set; } = new List<NetDto>();

        [JsonPropertyName("gpu")]
        public List<GpuDto>? Gpu { get; set; } = new List<GpuDto>();

        [JsonPropertyName("disk")]
        public List<DiskDto>? Disks { get; set; } = new List<DiskDto>();
    }
}
