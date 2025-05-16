using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class Monitor : InventStuff
    {
        public string? SerialNumber { get; set; }
        public int? IdWorkplace { get; set; }
        public virtual Workplace? Workplace { get; set; }
    }
}
