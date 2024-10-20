using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class Mouse
    {
        public int IdMouse { get; set; }
        public int IdPerifery { get; set; }
        public Perifery Perifery { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
    }


}
