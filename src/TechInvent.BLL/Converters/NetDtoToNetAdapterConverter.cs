using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters
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
                IdManufacturerNavigation = new Manufacturer { Name = source.Manufacturer },
                AdapterTypeIdAdapterTypeNavigation = new AdapterType { Name = source.AdapterType }
            };
        }
    }
}
