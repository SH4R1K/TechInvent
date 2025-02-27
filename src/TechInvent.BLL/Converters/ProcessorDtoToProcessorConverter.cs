using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters
{
    // Преобразование ProcessorDto -> Processor.
    public class ProcessorDtoToProcessorConverter : IConverter<ProcessorDto, Processor>
    {
        public Processor Convert(ProcessorDto source)
        {
            return new Processor
            {
                Name = source.Name,
                PhysicalCores = source.PhysicalCores.ToString(),
                LogicalCores = source.LogicalCores.ToString(),
                MaxClockSpeed = source.MaxClockSpeed.ToString()
            };
        }
    }
}
