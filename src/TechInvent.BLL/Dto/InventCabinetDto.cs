using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TechInvent.BLL.Dto
{
    public class InventCabinetDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("workplace")]
        public WorkplaceDto? Workplace { get; set; }
    }
}
