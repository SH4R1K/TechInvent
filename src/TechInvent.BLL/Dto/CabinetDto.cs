using Newtonsoft.Json;

namespace TechInvent.BLL.Dto
{
    public class CabinetDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("workplace")]
        public WorkplaceDto? Workplace { get; set; }
    }

}
