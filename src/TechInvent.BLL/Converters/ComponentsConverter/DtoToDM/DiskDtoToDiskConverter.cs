using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter.DtoToDM
{
    // Преобразование DiskDto -> Disk.
    public class DiskDtoToDiskConverter : IConverter<DiskDto, Disk>
    {
        public Disk Convert(DiskDto source)
        {
            return new Disk
            {
                Model = source.Model,
                Size = source.Size
            };
        }
    }
}
