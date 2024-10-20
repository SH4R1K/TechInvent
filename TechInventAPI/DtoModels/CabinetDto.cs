using Newtonsoft.Json;

namespace TechInventAPI.DtoModels
{
    public class CabinetDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("workplace")]
        public WorkplaceDto? Workplace { get; set; }
    }

}
