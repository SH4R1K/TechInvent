using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class WorkplaceDto
    {
        [JsonPropertyName("comp_name")]
        public string CompName { get; set; }

        [JsonPropertyName("os_name")]
        public string OsName { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("hardware_info")]
        public HardwareInfo? HardwareInfo { get; set; }
        [JsonPropertyName("software")]
        public List<SoftwareDto> Software { get; set; }
    }


}
