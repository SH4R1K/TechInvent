using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.InventDto
{
    // Преобразование HardwareInfo -> List<Component>
    // Каждый тип компонента конвертируется через свой конвертер.
    public class ComponentsConverter : IConverter<HardwareInfo, List<Component>>
    {
        private readonly IConverter<RamDto, Ram> _ramConverter;
        private readonly IConverter<GpuDto, Gpu> _gpuConverter;
        private readonly IConverter<ProcessorDto, Processor> _processorConverter;
        private readonly IConverter<NetDto, NetAdapter> _netAdapterConverter;
        private readonly IConverter<DiskDto, Disk> _diskConverter;
        private readonly IConverter<MainboardDto, Mainboard> _mainboardConverter;

        public ComponentsConverter(
            IConverter<RamDto, Ram> ramConverter,
            IConverter<GpuDto, Gpu> gpuConverter,
            IConverter<ProcessorDto, Processor> processorConverter,
            IConverter<NetDto, NetAdapter> netAdapterConverter,
            IConverter<DiskDto, Disk> diskConverter,
            IConverter<MainboardDto, Mainboard> mainboardConverter)
        {
            _ramConverter = ramConverter;
            _gpuConverter = gpuConverter;
            _processorConverter = processorConverter;
            _netAdapterConverter = netAdapterConverter;
            _diskConverter = diskConverter;
            _mainboardConverter = mainboardConverter;
        }

        public List<Component> Convert(HardwareInfo source)
        {
            var components = new List<Component>();

            if (source.Ram != null)
                components.AddRange(source.Ram.Select(r => _ramConverter.Convert(r)));
            if (source.Gpu != null)
                components.AddRange(source.Gpu.Select(g => _gpuConverter.Convert(g)));
            if (source.Processor != null)
                components.AddRange(source.Processor.Select(p => _processorConverter.Convert(p)));
            if (source.Net != null)
                components.AddRange(source.Net.Select(n => _netAdapterConverter.Convert(n)));
            if (source.Disks != null)
                components.AddRange(source.Disks.Select(d => _diskConverter.Convert(d)));
            if (source.Mainboard != null)
                components.Add(_mainboardConverter.Convert(source.Mainboard));

            return components;
        }
    }
}
