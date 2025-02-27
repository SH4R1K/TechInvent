using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters
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
                // Создаём производителя на основе vendor.
                // Обратите внимание: в реальном приложении может потребоваться механизм переиспользования,
                // чтобы не создавать дубликаты.
                ManufacturerNavigation = new Manufacturer { Name = source.Vendor }
            };
        }
    }
}
