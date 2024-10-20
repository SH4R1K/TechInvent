using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class WorkplaceDto
    {
        public int IdWorkplace { get; set; }
        [JsonProperty("comp_name")]
        public string CompName { get; set; }

        [JsonProperty("os_name")]
        public string OsName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("hardware_info")]
        public HardwareInfo? HardwareInfo { get; set; }
        public int IdCabinet { get; set; }
        public CabinetDto Cabinet { get; set; }
    }


}
