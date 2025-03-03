using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    public partial class GpuToGpuDtoConverter
    {
        public class RamToRamDtoConverter : IConverter<Ram, RamDto>
        {
            public RamDto Convert(Ram source)
            {
                return new RamDto
                {
                    Name = source.Name,
                    Speed = int.Parse(source.Speed),
                    Capacity = source.Capacity,
                    PartNumber = source.PartNumber,
                    SerialNumber = source.SerialNumber,
                    Manufacturer = source.Manufacturer.Name,
                };
            }
        }
    }
}
