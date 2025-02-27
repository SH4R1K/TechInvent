using Newtonsoft.Json;

namespace TechInvent.BLL.Dto
{
    public class NetDto
    {
        public int IdNet { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("mac_address")]
        public string MacAddress { get; set; }

        [JsonProperty("adapter_type")]
        public string AdapterType { get; set; }
    }


}
