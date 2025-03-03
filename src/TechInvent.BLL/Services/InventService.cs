using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Services
{
    public class InventService : IInventService
    {
        private readonly ICabinetRepository _cabinetRepository;
        private readonly IConverter<InventCabinetDto, Cabinet> _cabinetConverter;
        private readonly IConverter<Cabinet, InventCabinetDto> _cabinetDtoConverter;
        private readonly IConverter<WorkplaceDto, Workplace> _workplaceConverter;
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly IOsRepository _osRepository;
        private readonly ISoftwareService _softwareService;
        private readonly IComponentService _componentService;

        public InventService(
            IConverter<InventCabinetDto, Cabinet> cabinetConverter,
            IConverter<Cabinet, InventCabinetDto> cabinetDtoConverter,
            IConverter<WorkplaceDto, Workplace> workplaceConverter,
            ICabinetRepository cabinetRepository,
            IWorkplaceRepository workplaceRepository,
            IOsRepository osRepository,
            ISoftwareService softwareService,
            IComponentService componentService)
        {
            _cabinetRepository = cabinetRepository;
            _cabinetConverter = cabinetConverter;
            _cabinetDtoConverter = cabinetDtoConverter;
            _workplaceRepository = workplaceRepository;
            _workplaceConverter = workplaceConverter;
            _osRepository = osRepository;
            _softwareService = softwareService;
            _componentService = componentService;
        }
        public async Task<InventCabinetDto> InventWorkplace(InventCabinetDto cabinetDto)
        {
            var cabinet = _cabinetConverter.Convert(cabinetDto);
            cabinet.Workplaces = null;
            var existCabinet = await _cabinetRepository.FindAsync(c => c.Name == cabinetDto.Name);
            if (existCabinet == null)
                existCabinet = await _cabinetRepository.AddAsync(cabinet);

            var existWorkplace = await _workplaceRepository.FindAsync(c => c.Name == cabinetDto.Workplace.CompName);
            var workplace = _workplaceConverter.Convert(cabinetDto.Workplace);
            workplace.Os = await _osRepository.GetOrCreateAsync(workplace.Os.OsName, workplace.Os.OsVersion);
            workplace.IdCabinet = existCabinet.IdCabinet;
            workplace.Components = await _componentService.CheckComponentsAsync(workplace.Components.ToList());
            workplace.InstalledSoftware = await _softwareService.CheckSoftwareAsync(workplace.InstalledSoftware.ToList(), workplace);
            if (existWorkplace == null)
                existWorkplace = await _workplaceRepository.AddAsync(workplace);
            else
                await _workplaceRepository.UpdateAsync(existWorkplace.IdWorkplace, workplace);
            return _cabinetDtoConverter.Convert(existCabinet);
        }
    }
}
