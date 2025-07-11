﻿using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Cabinet
{
    public int IdCabinet { get; set; }

    public required string Name { get; set; }

    public virtual List<Workplace> Workplaces { get; set; } = new List<Workplace>();
    public virtual List<CabinetEquipment> CabinetEquipments { get; set;} = new List<CabinetEquipment>();
}
