using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.InventDto
{
    // Преобразование SoftwareDto -> Software.
    public class SoftwareDtoToSoftwareConverter : IConverter<SoftwareDto, Software>
    {
        public Software Convert(SoftwareDto source)
        {
            return new Software
            {
                Name = source.Name,
                Version = source.Version,
                Manufacturer = new Manufacturer { Name = source.Vendor }
            };
        }
    }
}
