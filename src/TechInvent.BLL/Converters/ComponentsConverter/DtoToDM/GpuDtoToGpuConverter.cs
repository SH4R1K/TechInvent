using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter.DtoToDM
{
    // Преобразование GpuDto -> Gpu.
    public class GpuDtoToGpuConverter : IConverter<GpuDto, Gpu>
    {
        public Gpu Convert(GpuDto source)
        {
            return new Gpu
            {
                Name = source.Name,
                AdapterRam = source.AdapterRam.ToString(),
                Uuid = source.Uuid
            };
        }
    }
}
