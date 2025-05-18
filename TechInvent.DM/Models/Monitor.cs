using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class Monitor : InventStuff
    {
        public int? IdWorkplace { get; set; }
        public int? IdVendor { get; set; }
        public virtual Workplace? Workplace { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
