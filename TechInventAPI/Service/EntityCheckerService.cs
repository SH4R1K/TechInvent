using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TechInventAPI.Data;
using TechInventAPI.Models;

namespace TechInventAPI.Service
{
    public class EntityCheckerService
    {
        private readonly TechInventContext _context;

        public EntityCheckerService(TechInventContext context)
        {
            _context = context;
        }

        public Os GetOrCreateOs(string osName, string osVersion)
        {
            var os = _context.Os.AsNoTracking().FirstOrDefault(o => o.OsName == osName && o.OsVersion == osVersion);
            if (os == null)
            {
                os = new Os { OsName = osName, OsVersion = osVersion };
                _context.Os.Add(os);
                SaveChanges();
            }
            return os;
        }

        public Manufacturer GetOrCreateManufacturer(string name)
        {
            var manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Name == name);
            if (manufacturer == null)
            {
                manufacturer = new Manufacturer { Name = name };
                _context.Manufacturers.Add(manufacturer);
                SaveChanges();
            }
            return manufacturer;
        }

        public AdapterType GetOrCreateAdapterType(string name)
        {
            var adapterType = _context.AdapterTypes.FirstOrDefault(m => m.Name == name);
            if (adapterType == null)
            {
                adapterType = new AdapterType { Name = name };
                _context.AdapterTypes.Add(adapterType);
                SaveChanges();
            }
            return adapterType;
        }

        public Workplace GetOrCreateWorkplace(string name, Cabinet cabinet, Os os, List<Component> components, List<Software> software)
        {
            var workplace = _context.Workplaces.Include(w => w.Components).Include(w => w.InstalledSoftware).FirstOrDefault(m => m.Name == name);
            if (workplace == null)
            {
                workplace = new Workplace { Name = name, IdCabinetNavigation = cabinet, IdOsNavigation = os };
                _context.Workplaces.Add(workplace);
            }
            else
            {
                workplace.LastUpdate = DateTime.UtcNow;
                workplace.IdCabinetNavigation = cabinet;
                workplace.IdOsNavigation = os;
                workplace.Components.Clear();
                workplace.InstalledSoftware.Clear();
            }

            foreach (var component in components)
            {
                component.IdWorkplaceNavigation = workplace;
                workplace.Components.Add(component);
            }
            workplace.InstalledSoftware.AddRange(software.DistinctBy(s => s.IdSoftware).Select(s => new InstalledSoftware { IdSoftware = s.IdSoftware, IdWorkplace = workplace.IdWorkplace }).ToList());
            //_context.Update(workplace);
            return workplace;
        }

        public Cabinet GetOrCreateCabinet(string name)
        {
            var cabinet = _context.Cabinets.AsNoTracking().FirstOrDefault(m => m.Name == name);
            if (cabinet == null)
            {
                cabinet = new Cabinet { Name = name };
                _context.Cabinets.Add(cabinet);
                SaveChanges();
            }
            return cabinet;
        }

        public Software GetOrCreateSoftware(string name, string version, int idManufacturer)
        {
            var software = _context.Softwares.AsNoTracking().FirstOrDefault(s => s.Name == name && s.Version == version && s.IdManufacturer == s.IdManufacturer);
            if (software == null)
            {
                software = new Software { Name = name, Version = version, IdManufacturer = idManufacturer };
                _context.Softwares.Add(software);
                SaveChanges();
            }
            return software;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }


}
