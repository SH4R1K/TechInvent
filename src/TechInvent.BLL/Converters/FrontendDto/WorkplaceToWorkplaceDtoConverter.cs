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
    public class WorkplaceToWorkplaceDtoConverter : IConverter<Workplace, WorkplaceDto>
    {
        public WorkplaceToWorkplaceDtoConverter()
        {
        }

        public WorkplaceDto Convert(Workplace source)
        {

            return new WorkplaceDto
            {
                Id = source.IdWorkplace,
                CompName = source.Name,
                OsName = source.Os.OsName,
                Version = source.Os.OsVersion,
                Software = new List<SoftwareDto>(),
                HardwareInfo = new HardwareInfo()
            };
        }
    }
}
