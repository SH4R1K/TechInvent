using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Service
{
    public class EntityCheckerService
    {
        private readonly TechInventContext _context;

        public EntityCheckerService(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Os> GetOrCreateOsAsync(string osName, string osVersion)
        {
            var os = await _context.Os.FirstOrDefaultAsync(o => o.OsName == osName && o.OsVersion == osVersion);
            if (os == null)
            {
                os = new Os { OsName = osName, OsVersion = osVersion };
                await _context.Os.AddAsync(os);
                await SaveChangesAsync();
            }
            return os;
        }

        public async Task<Manufacturer> GetOrCreateManufacturerAsync(string name)
        {
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name);
            if (manufacturer == null)
            {
                manufacturer = new Manufacturer { Name = name };
                await _context.Manufacturers.AddAsync(manufacturer);
                await SaveChangesAsync();
            }
            return manufacturer;
        }

        public async Task<AdapterType> GetOrCreateAdapterTypeAsync(string name)
        {
            var adapterType = await _context.AdapterTypes.FirstOrDefaultAsync(m => m.Name == name);
            if (adapterType == null)
            {
                adapterType = new AdapterType { Name = name };
                await _context.AdapterTypes.AddAsync(adapterType);
                await SaveChangesAsync();
            }
            return adapterType;
        }

        public async Task<Workplace> GetOrCreateWorkplaceAsync(string name, Cabinet cabinet, Os os, List<Component> components, List<Software> software)
        {
            var workplace = await _context.Workplaces
                .Include(w => w.Components)
                .Include(w => w.InstalledSoftware)
                .FirstOrDefaultAsync(m => m.Name == name && !m.IsDecommissioned);

            if (workplace == null)
            {
                workplace = new Workplace
                {
                    Name = name,
                    IdCabinetNavigation = cabinet,
                    IdOsNavigation = os
                };
                await _context.Workplaces.AddAsync(workplace);
            }
            else
            {
                workplace.LastUpdate = DateTime.UtcNow;
                workplace.IdCabinetNavigation = cabinet;
                workplace.IdOsNavigation = os;
                workplace.Components.RemoveAll(c => !components.Contains(c));
                workplace.InstalledSoftware.Clear();
            }

            var filteredComponents = components.Where(c => !workplace.Components.Contains(c));
            foreach (var component in filteredComponents)
            {
                component.IdWorkplaceNavigation = workplace;
                workplace.Components.Add(component);
            }

            workplace.InstalledSoftware.AddRange(
                software.DistinctBy(s => s.IdSoftware)
                    .Select(s => new InstalledSoftware
                    {
                        IdSoftware = s.IdSoftware,
                        IdWorkplace = workplace.IdInventStuff
                    }).ToList()
            );

            return workplace;
        }

        public async Task<Cabinet> GetOrCreateCabinetAsync(string name)
        {
            var cabinet = await _context.Cabinets.FirstOrDefaultAsync(m => m.Name == name);
            if (cabinet == null)
            {
                cabinet = new Cabinet { Name = name };
                await _context.Cabinets.AddAsync(cabinet);
                await SaveChangesAsync();
            }
            return cabinet;
        }

        public async Task<Software> GetOrCreateSoftwareAsync(string name, string version, int idManufacturer)
        {
            var software = await _context.Softwares.FirstOrDefaultAsync(s =>
                s.Name == name && s.Version == version && s.IdManufacturer == idManufacturer);

            if (software == null)
            {
                software = new Software
                {
                    Name = name,
                    Version = version,
                    IdManufacturer = idManufacturer
                };
                await _context.Softwares.AddAsync(software);
                await SaveChangesAsync();
            }
            return software;
        }

        public async Task<CabinetEquipmentType> GetOrCreateCabinetEquipmentTypeAsync(string typeName)
        {
            var type = await _context.CabinetEquipmentTypes.FirstOrDefaultAsync(t => t.Name == typeName);
            if (type == null)
            {
                type = new CabinetEquipmentType { Name = typeName };
                await _context.CabinetEquipmentTypes.AddAsync(type);
                await SaveChangesAsync();
            }
            return type;
        }

        public async Task<Vendor> GetOrCreateVendorAsync(string typeName)
        {
            var vendor = await _context.Vendors.FirstOrDefaultAsync(t => t.Name == typeName);
            if (vendor == null)
            {
                vendor = new Vendor { Name = typeName };
                await _context.Vendors.AddAsync(vendor);
                await SaveChangesAsync();
            }
            return vendor;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
