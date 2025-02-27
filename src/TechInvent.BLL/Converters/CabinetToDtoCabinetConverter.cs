using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters
{
    public class CabinetToDtoCabinetConverter : IConverter<Cabinet, CabinetDto>
    {
        public CabinetDto Convert(Cabinet source)
        {
            return new CabinetDto {  Name =  source.Name };
        }
    }
}
