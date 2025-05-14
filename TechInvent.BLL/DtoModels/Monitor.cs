
using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class Monitor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("screen_width")]
        public int? ScreenWidth { get; set; }

        [JsonPropertyName("screen_height")]
        public int? ScreenHeight { get; set; }
    }


}
