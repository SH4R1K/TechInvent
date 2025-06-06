using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class RequestType
    {
        public int IdRequestType { get; set; }
        public required string Name { get; set; }
        public List<TechRequest> TechRequests { get; set; } = new List<TechRequest>();
    }
}
