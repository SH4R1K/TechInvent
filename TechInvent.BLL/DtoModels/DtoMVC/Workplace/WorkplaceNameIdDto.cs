﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.BLL.DtoModels.DtoMVC.Workplace
{
    public class WorkplaceNameIdDto
    {
        public int? IdWorkplace { get; set; }
        public required string Name { get; set; }
    }
}
