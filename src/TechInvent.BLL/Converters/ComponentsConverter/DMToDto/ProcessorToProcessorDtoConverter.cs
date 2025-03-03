using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.ComponentsConverter
{
    public partial class GpuToGpuDtoConverter
    {
        public class ProcessorToProcessorDtoConverter : IConverter<Processor, ProcessorDto>
        {
            public ProcessorDto Convert(Processor source)
            {
                return new ProcessorDto
                {
                    Name = source.Name,
                    PhysicalCores = int.Parse(source.PhysicalCores),
                    LogicalCores = int.Parse(source.LogicalCores),
                    MaxClockSpeed = int.Parse(source.MaxClockSpeed)
                };
            }
        }
    }
}
