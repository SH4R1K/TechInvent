using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class TechRequestWorkplace
    {
        public int IdWorkplace { get; set; }
        public int IdTechRequest { get; set; }
        public bool IsActive { get; set; } = true;
        public required Workplace Workplace { get; set; }
        public required TechRequest TechRequest { get; set; }
    }
}
