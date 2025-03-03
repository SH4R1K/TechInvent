using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter.DtoToDM
{
    // Преобразование NetDto -> NetAdapter.
    public class NetDtoToNetAdapterConverter : IConverter<NetDto, NetAdapter>
    {
        public NetAdapter Convert(NetDto source)
        {
            return new NetAdapter
            {
                Name = source.Name,
                MacAddress = source.MacAddress,
                Manufacturer = new Manufacturer { Name = source.Manufacturer },
                AdapterType = new AdapterType { Name = source.AdapterType }
            };
        }
    }
}
