using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.FrontendDto
{
    public class CabinetToDtoCabinetConverter : IConverter<Cabinet, CabinetDto>
    {
        private readonly IConverter<Workplace, WorkplaceDto> _workplaceConverter;
        public CabinetToDtoCabinetConverter(IConverter<Workplace, WorkplaceDto> workplaceConverter)
        {
            _workplaceConverter = workplaceConverter;
        }
        public CabinetDto Convert(Cabinet source)
        {
            return new CabinetDto { Id = source.IdCabinet, Name = source.Name, Workplaces = source.Workplaces.Select(_workplaceConverter.Convert).ToList() };
        }
    }
}
