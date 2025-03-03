using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter.DtoToDM
{
    // Преобразование MainboardDto -> Mainboard.
    public class MainboardDtoToMainboardConverter : IConverter<MainboardDto, Mainboard>
    {
        public Mainboard Convert(MainboardDto source)
        {
            return new Mainboard
            {
                Name = source.Name,
                SerialNumber = source.SerialNumber
            };
        }
    }
}
