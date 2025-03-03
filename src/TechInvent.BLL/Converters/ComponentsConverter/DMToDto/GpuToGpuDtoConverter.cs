using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    // Преобразование GpuDto -> Gpu.
    public partial class GpuToGpuDtoConverter : IConverter<Gpu, GpuDto>
    {
        public GpuDto Convert(Gpu source)
        {
            return new GpuDto
            {
                Id = source.IdComponent,
                Name = source.Name,
                AdapterRam = double.Parse(source.AdapterRam),
                Uuid = source.Uuid
            };
        }
    }
}
