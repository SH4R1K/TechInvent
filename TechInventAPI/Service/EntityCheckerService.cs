using Microsoft.EntityFrameworkCore;
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
            var os = _context.Os.FirstOrDefault(o => o.OsName == osName && o.OsVersion == osVersion);
            if (os == null)
            {
                os = new Os { OsName = osName, OsVersion = osVersion };
                _context.Os.Add(os);
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
            }
            return adapterType;
        }

        public Workplace GetOrCreateWorkplace(string name, Cabinet cabinet, Os os, List<Component> components)
        {
            var workplace = _context.Workplaces.Include(w => w.Components).FirstOrDefault(m => m.Name == name);
            if (workplace == null)
            {
                workplace = new Workplace { Name = name, IdCabinetNavigation = cabinet, IdOsNavigation = os };
                _context.Workplaces.Add(workplace);
            }
            else
            {
                workplace.IdCabinetNavigation = cabinet;
                workplace.IdOsNavigation = os;
                workplace.Components.Clear();
            }

            foreach (var component in components)
            {
                component.IdWorkplaceNavigation = workplace;
                workplace.Components.Add(component);
            }

            _context.Update(workplace);
            return workplace;
        }

        public Cabinet GetOrCreateCabinet(string name)
        {
            var cabinet = _context.Cabinets.FirstOrDefault(m => m.Name == name);
            if (cabinet == null)
            {
                cabinet = new Cabinet { Name = name };
                _context.Cabinets.Add(cabinet);
            }
            return cabinet;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }


}
