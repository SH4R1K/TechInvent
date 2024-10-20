using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class Perifery
    {
        public int IdPerifery { get; set; }
        public int IdHardwareInfo { get; set; }
        public HardwareInfo HardwareInfo { get; set; }
        [JsonProperty("monitors")]
        public List<Monitor> Monitors { get; set; }

        [JsonProperty("mice")]
        public List<Mouse> Mice { get; set; }

        [JsonProperty("keyboards")]
        public List<Keyboard> Keyboards { get; set; }

        [JsonProperty("printers")]
        public List<Printer> Printers { get; set; }
    }


}
