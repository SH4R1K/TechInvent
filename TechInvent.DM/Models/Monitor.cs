using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class Monitor
    {
        public int IdMonitor { get; set; }
        public required string Name { get; set; }
        public string? InventNumber { get; set; }
        public int? IdWorkplace { get; set; }
        public virtual Workplace? Workplace { get; set; }
    }
}
