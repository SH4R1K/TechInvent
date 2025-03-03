using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    public partial class GpuToGpuDtoConverter
    {
        public class NetToNetAdapterDtoConverter : IConverter<NetAdapter, NetDto>
        {
            public NetDto Convert(NetAdapter source)
            {
                return new NetDto
                {
                    Name = source.Name,
                    MacAddress = source.MacAddress,
                    Manufacturer = source.Manufacturer.Name,
                    AdapterType = source.AdapterType.Name
                };
            }
        }
    }
}
