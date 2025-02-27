using Newtonsoft.Json;

namespace TechInvent.BLL.Dto
{
    public class MainboardDto
    {
        public int IdMainboard { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }
    }


}
