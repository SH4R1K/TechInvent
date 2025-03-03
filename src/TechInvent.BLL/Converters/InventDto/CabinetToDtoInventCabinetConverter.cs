using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.InventDto
{
    public class CabinetToDtoInventCabinetConverter : IConverter<Cabinet, InventCabinetDto>
    {
        public InventCabinetDto Convert(Cabinet source)
        {
            return new InventCabinetDto { Id = source.IdCabinet, Name = source.Name };
        }
    }
}
