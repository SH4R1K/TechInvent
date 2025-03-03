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
    public class CabinetDtoToCabinetConverter : IConverter<InventCabinetDto, Cabinet>
    {
        private readonly IConverter<WorkplaceDto, Workplace> _workplaceConverter;

        public CabinetDtoToCabinetConverter(IConverter<WorkplaceDto, Workplace> workplaceConverter)
        {
            _workplaceConverter = workplaceConverter;
        }

        public Cabinet Convert(InventCabinetDto source)
        {
            var cabinet = new Cabinet { Name = source.Name };

            if (source.Workplace != null)
            {
                var workplace = _workplaceConverter.Convert(source.Workplace);
                workplace.Cabinet = cabinet;
                cabinet.Workplaces = new List<Workplace> { workplace };
            }

            return cabinet;
        }
    }
}
