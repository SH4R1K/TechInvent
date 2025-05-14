using System.Text.Json.Serialization;

namespace TechInvent.BLL.DtoModels
{
    public class CabinetDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("workplace")]
        public WorkplaceDto? Workplace { get; set; }
    }

}
