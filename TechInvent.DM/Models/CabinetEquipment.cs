using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DM.Models
{
    public class CabinetEquipment
    {
        public int IdCabinetEquipment { get; set; }
        public required string Name { get; set; }
        public string? InventNumber { get; set; }
        public int? IdCabinet { get; set; }
        public int IdCabinetEquipmentType { get; set; }
        public required virtual CabinetEquipmentType CabinetEquipmentType { get; set; }
        public virtual Cabinet? Cabinet { get; set; }
    }
}
