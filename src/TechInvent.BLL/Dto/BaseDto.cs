using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechInvent.BLL.Dto
{
    public class BaseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
