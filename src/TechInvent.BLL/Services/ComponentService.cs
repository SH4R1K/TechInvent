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
    public class ComponentService : IComponentService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IAdapterTypeRepository _adapterTypeRepository;

        public ComponentService(IManufacturerRepository manufacturerRepository, IAdapterTypeRepository adapterTypeRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _adapterTypeRepository = adapterTypeRepository;
        }

        public async Task<List<Component>> CheckComponentsAsync(List<Component> components)
        {
            foreach (var component in components)
            {
                if (component is NetAdapter)
                {
                    NetAdapter netAdapter = component as NetAdapter;
                    netAdapter.IdManufacturer = (await _manufacturerRepository.GetOrCreateAsync((netAdapter.Manufacturer.Name!))).IdManufacturer;
                    netAdapter.IdAdapterType = (await _adapterTypeRepository.GetOrCreateAsync((netAdapter.AdapterType.Name!))).IdAdapterType;
                    netAdapter.Manufacturer = null;
                    netAdapter.AdapterType = null;
                }
                else if (component is Ram)
                {
                    Ram ram = component as Ram;
                    ram.IdManufacturer = (await _manufacturerRepository.GetOrCreateAsync(((Ram)component).Manufacturer.Name!)).IdManufacturer;
                    ram.Manufacturer = null;
                }
            }
            return components;
        }
    }
}
