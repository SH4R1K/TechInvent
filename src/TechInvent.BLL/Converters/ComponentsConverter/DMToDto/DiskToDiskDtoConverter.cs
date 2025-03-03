using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    // Преобразование DiskDto -> Disk.
    public class DiskToDiskDtoConverter : IConverter<Disk, DiskDto>
    {
        public DiskDto Convert(Disk source)
        {
            return new DiskDto
            {
                Id = source.IdComponent,
                Model = source.Model,
                Size = source.Size
            };
        }
    }
}
