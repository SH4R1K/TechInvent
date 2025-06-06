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
        public Workplace Workplace { get; set; }
        public TechRequest TechRequest { get; set; }
    }
}
