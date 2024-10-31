using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class DiskDto
    {
        public int IdDisk { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
