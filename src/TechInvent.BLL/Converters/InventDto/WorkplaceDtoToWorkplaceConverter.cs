using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Converters.InventDto
{
    public class WorkplaceDtoToWorkplaceConverter : IConverter<WorkplaceDto, Workplace>
    {
        private readonly IConverter<HardwareInfo, List<Component>> _componentsConverter;
        private readonly IConverter<SoftwareDto, Software> _softwareConverter;

        public WorkplaceDtoToWorkplaceConverter(
            IConverter<HardwareInfo, List<Component>> componentsConverter,
            IConverter<SoftwareDto, Software> softwareConverter)
        {
            _componentsConverter = componentsConverter;
            _softwareConverter = softwareConverter;
        }

        public Workplace Convert(WorkplaceDto source)
        {
            // Создаём ОС
            var os = new Os
            {
                OsName = source.OsName,
                OsVersion = source.Version
            };

            // Конвертируем аппаратную часть (компоненты)
            var components = _componentsConverter.Convert(source.HardwareInfo);

            // Конвертируем ПО
            var softwareList = source.Software
                .Select(s => _softwareConverter.Convert(s))
                .ToList();

            return new Workplace
            {
                Name = source.CompName,
                Os = os,
                Components = components,
                // Преобразуем список ПО в коллекцию установленных программ
                InstalledSoftware = softwareList
                    .Select(s => new InstalledSoftware { Software = s })
                    .ToList()
            };
        }
    }
}
