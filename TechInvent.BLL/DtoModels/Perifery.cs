
using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class Perifery
    {
        [JsonPropertyName("monitors")]
        public List<Monitor> Monitors { get; set; }

        [JsonPropertyName("mice")]
        public List<Mouse> Mice { get; set; }

        [JsonPropertyName("keyboards")]
        public List<Keyboard> Keyboards { get; set; }

        [JsonPropertyName("printers")]
        public List<Printer> Printers { get; set; }
    }


}
