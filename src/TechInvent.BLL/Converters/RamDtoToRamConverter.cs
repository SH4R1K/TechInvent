using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters
{
    // Преобразование RamDto -> Ram.
    public class RamDtoToRamConverter : IConverter<RamDto, Ram>
    {
        public Ram Convert(RamDto source)
        {
            return new Ram
            {
                Name = source.Name,
                Speed = source.Speed.ToString(),
                Capacity = source.Capacity,
                MemoryType = source.MemoryType.ToString(),
                PartNumber = source.PartNumber,
                SerialNumber = source.SerialNumber,
                IdManufacturerNavigation = new Manufacturer { Name = source.Manufacturer }
            };
        }
    }
}
