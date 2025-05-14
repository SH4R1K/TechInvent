using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class CabinetEquipmentType
    {
        public int IdCabinetEquipmentType { get; set; }
        public required string Name { get; set; }
        public virtual List<CabinetEquipment> CabinetEquipments { get; set; } = new List<CabinetEquipment>();
    }
}
