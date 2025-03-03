using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Dto
{
    public class CabinetDto : BaseDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("workplaces")]
        public List<WorkplaceDto>? Workplaces { get; set; } = new List<WorkplaceDto>();
    }
}
