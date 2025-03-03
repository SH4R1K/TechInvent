using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DAL.Interfaces;
using TechInvent.DAL.Repositories;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Services
{
    public class CabinetService : ICabinetService
    {
        private readonly ICabinetRepository _cabinetRepository;
        private readonly IConverter<InventCabinetDto, Cabinet> _cabinetConverter;
        private readonly IConverter<Cabinet, InventCabinetDto> _cabinetDtoInventConverter;
        private readonly IConverter<Cabinet, CabinetDto> _cabinetDtoConverter;

        public CabinetService(
            ICabinetRepository cabinetRepository,
            IConverter<InventCabinetDto, Cabinet> cabinetConverter,
            IConverter<Cabinet, CabinetDto> cabinetDtoConverter,
            IWorkplaceRepository workplaceRepository,
            IConverter<WorkplaceDto, Workplace> workplaceConverter,
            IConverter<Cabinet, InventCabinetDto> cabinetDtoInventConverter)
        {
            _cabinetRepository = cabinetRepository;
            _cabinetConverter = cabinetConverter;
            _cabinetDtoConverter = cabinetDtoConverter;
            _cabinetDtoInventConverter = cabinetDtoInventConverter;
        }

        public async Task<InventCabinetDto?> AddAsync(InventCabinetDto cabinetDto)
        {
            var cabinet = _cabinetConverter.Convert(cabinetDto);
            var result = await _cabinetRepository.AddAsync(cabinet);
            return result != null ? _cabinetDtoInventConverter.Convert(result) : null;
        }

        public async Task DeleteAsync(int id)
        {
            await _cabinetRepository.DeleteAsync(id);
        }

        public async Task<List<CabinetDto>> GetAllAsync()
        {
            var result = await _cabinetRepository.GetAllAsync();
            return result?.Select(_cabinetDtoConverter.Convert).ToList() ?? new List<CabinetDto>();
        }

        public async Task<InventCabinetDto?> GetByIdAsync(int id)
        {
            var cabinet = await _cabinetRepository.GetByIdAsync(id);
            return cabinet != null ? _cabinetDtoInventConverter.Convert(cabinet) : null;
        }

        public async Task<InventCabinetDto?> UpdateAsync(int id, InventCabinetDto cabinetDto)
        {
            var cabinet = _cabinetConverter.Convert(cabinetDto);
            var result = await _cabinetRepository.UpdateAsync(id, cabinet);
            return result != null ? _cabinetDtoInventConverter.Convert(result) : null;
        }
    }

}
