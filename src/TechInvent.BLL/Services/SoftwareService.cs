using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Interfaces;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISoftwareRepository _softwareRepository;

        public SoftwareService(ISoftwareRepository softwareRepository, IManufacturerRepository manufacturerRepository)
        {
            _softwareRepository = softwareRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<List<InstalledSoftware>> CheckSoftwareAsync(List<InstalledSoftware> software, Workplace workplace)
        {
            var updatedSoftwareList = new List<InstalledSoftware>();

            foreach (var soft in software)
            {
                // Получаем или создаем производителя
                var manufacturer = await _manufacturerRepository.GetOrCreateAsync(soft.Software.Manufacturer.Name!);

                // Получаем или создаем ПО
                var existingSoftware = await _softwareRepository.GetOrCreate(soft.Software.Name, soft.Software.Version, manufacturer);

                // Добавляем в список обновленных программ
                updatedSoftwareList.Add(new InstalledSoftware
                {
                    Workplace = workplace,
                    IdSoftware = existingSoftware.IdSoftware,
                    Software = existingSoftware // Сохраняем навигационное свойство, если оно используется
                });
            }

            return updatedSoftwareList.DistinctBy(s => s.IdSoftware).ToList();
        }
    }
}
