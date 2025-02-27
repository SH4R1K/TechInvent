using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Services
{
    public class CabinetService : ICabinetService
    {
        private readonly ICabinetRepository _cabinetRepository;
        private readonly IConverter<CabinetDto, Cabinet> _cabinetConverter;
        private readonly IConverter<Cabinet, CabinetDto> _cabinetDtoConverter;
        public CabinetService(ICabinetRepository cabinetRepository, IConverter<CabinetDto, Cabinet> cabinetConverter, IConverter<Cabinet, CabinetDto> cabinetDtoConverter)
        {
            _cabinetRepository = cabinetRepository;
            _cabinetConverter = cabinetConverter;
            _cabinetDtoConverter = cabinetDtoConverter;
        }
        public async Task<CabinetDto?> CreateAsync(CabinetDto cabinetDto)
        {
            var cabinet = _cabinetConverter.Convert(cabinetDto);
            if (_cabinetRepository.FindAsync(c => c.Name == cabinetDto.Name) == null)
            {
                var result = await _cabinetRepository.CreateAsync(cabinet);
            }
            return cabinetDto;
        }

        public async Task DeleteAsync(int id)
        {
            await _cabinetRepository.DeleteAsync(id);
        }

        public async Task<List<CabinetDto>?> GetAllAsync()
        {
            var result = await _cabinetRepository.GetAllAsync();
            return result.Select(_cabinetDtoConverter.Convert).ToList();
        }

        public async Task<CabinetDto?> GetByIdAsync(int id)
        {
            return _cabinetDtoConverter.Convert(await _cabinetRepository.GetByIdAsync(id));
        }

        public async Task<CabinetDto?> UpdateAsync(int id, CabinetDto cabinetDto)
        {
            var result = await _cabinetRepository.UpdateAsync(id, _cabinetConverter.Convert(cabinetDto));
            return _cabinetDtoConverter.Convert(result);
        }
    }
}
