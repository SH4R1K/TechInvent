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
    internal class CabinetDtoToCabinetConverter: IConverter<CabinetDto, Cabinet>
    {
        private readonly IConverter<WorkplaceDto, Workplace> _workplaceConverter;

        public CabinetDtoToCabinetConverter(IConverter<WorkplaceDto, Workplace> workplaceConverter)
        {
            _workplaceConverter = workplaceConverter;
        }

        public Cabinet Convert(CabinetDto source)
        {
            var cabinet = new Cabinet { Name = source.Name };

            if (source.Workplace != null)
            {
                var workplace = _workplaceConverter.Convert(source.Workplace);
                workplace.IdCabinetNavigation = cabinet;
                cabinet.Workplaces = new List<Workplace> { workplace };
            }

            return cabinet;
        }
    }
}
