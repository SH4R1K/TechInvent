using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    public partial class GpuToGpuDtoConverter
    {
        public class MainboardToMainboardDtoConverter : IConverter<Mainboard, MainboardDto>
        {
            public MainboardDto Convert(Mainboard source)
            {
                return new MainboardDto
                {
                    Name = source.Name,
                    SerialNumber = source.SerialNumber
                };
            }
        }
    }
}
