using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class CabinetEquipment : InventStuff
    {
        public int? IdCabinet { get; set; }
        public int? IdWorkplace { get; set; }
        public int? IdVendor { get; set; }
        public int IdCabinetEquipmentType { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public required virtual CabinetEquipmentType CabinetEquipmentType { get; set; }
        public virtual Workplace? Workplace { get; set; }
        public virtual Cabinet? Cabinet { get; set; }
    }
}
