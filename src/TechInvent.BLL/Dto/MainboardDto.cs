using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class MainboardDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("serial_number")]
        public string SerialNumber { get; set; }
    }


}
