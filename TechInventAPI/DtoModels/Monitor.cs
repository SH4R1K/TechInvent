using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class Monitor
    {
        public int IdMonitor { get; set; }
        public int IdPerifery { get; set; }
        public Perifery Perifery { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("screen_width")]
        public int? ScreenWidth { get; set; }

        [JsonProperty("screen_height")]
        public int? ScreenHeight { get; set; }
    }


}
