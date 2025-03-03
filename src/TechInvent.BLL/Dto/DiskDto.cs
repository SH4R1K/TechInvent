using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class DiskDto : BaseDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}
