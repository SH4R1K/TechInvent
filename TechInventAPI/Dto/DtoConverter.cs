using TechInventAPI.DtoModels;
using TechInventAPI.Models;
using TechInventAPI.Service;
using Cabinet = TechInventAPI.Models.Cabinet;

namespace TechInventAPI.Dto
{
    public class DtoConverter
    {
        private readonly EntityCheckerService _entityChecker;

        public DtoConverter(EntityCheckerService entityChecker)
        {
            _entityChecker = entityChecker;
        }

        public Cabinet ConvertDtoCabinet(CabinetDto cabinetDto)
        {
            Cabinet cabinet = _entityChecker.GetOrCreateCabinet(cabinetDto.Name);
            ConvertDtoWorkPlace(cabinetDto.Workplace, cabinet);
            _entityChecker.SaveChanges();
            return cabinet;
        }

        public Workplace ConvertDtoWorkPlace(WorkplaceDto workplaceDto, Cabinet cabinet)
        {
            var os = _entityChecker.GetOrCreateOs(workplaceDto.OsName, workplaceDto.Version);
            List<Component> components = ConvertDtoComponents(workplaceDto.HardwareInfo);
            var workplace = _entityChecker.GetOrCreateWorkplace(workplaceDto.CompName, cabinet, os, components);
            return workplace;
        }

        public List<Component> ConvertDtoComponents(HardwareInfo hardwareInfo)
        {
            var components = new List<Component>();

            components.AddRange(hardwareInfo.Ram.Select(ConvertDtoRam));
            components.AddRange(hardwareInfo.Gpu.Select(ConvertDtoGpu));
            components.AddRange(hardwareInfo.Processor.Select(ConvertDtoProcessor));
            components.AddRange(hardwareInfo.Net.Select(ConvertDtoNetAdapter));

            components.Add(ConvertDtoMainboard(hardwareInfo.Mainboard));
            return components;
        }

        public Ram ConvertDtoRam(RamDto ramDto)
        {
            return new Ram
            {
                Name = ramDto.Name,
                Speed = ramDto.Speed.ToString(),
                Capacity = ramDto.Capacity,
                MemoryType = ramDto.MemoryType.ToString(),
                PartNumber = ramDto.PartNumber,
                SerialNumber = ramDto.SerialNumber,
                IdManufacturerNavigation = _entityChecker.GetOrCreateManufacturer(ramDto.Manufacturer)
            };
        }

        public Mainboard ConvertDtoMainboard(MainboardDto mainboardDto)
        {
            return new Mainboard
            {
                Name = mainboardDto.Name,
                SerialNumber = mainboardDto.SerialNumber
            };
        }

        public Processor ConvertDtoProcessor(ProcessorDto processorDto)
        {
            return new Processor
            {
                Name = processorDto.Name,
                PhysicalCores = processorDto.PhysicalCores.ToString(),
                LogicalCores = processorDto.LogicalCores.ToString(),
                MaxClockSpeed = processorDto.MaxClockSpeed.ToString()
            };
        }

        public NetAdapter ConvertDtoNetAdapter(NetDto netDto)
        {
            return new NetAdapter
            {
                Name = netDto.Name,
                MacAddress = netDto.MacAddress,
                IdManufacturerNavigation = _entityChecker.GetOrCreateManufacturer(netDto.Manufacturer),
                AdapterTypeIdAdapterTypeNavigation = _entityChecker.GetOrCreateAdapterType(netDto.AdapterType)
            };
        }

        public Gpu ConvertDtoGpu(GpuDto gpuDto)
        {
            return new Gpu
            {
                Name = gpuDto.Name,
                AdapterRam = gpuDto.AdapterRam.ToString(),
                Uuid = gpuDto.Uuid
            };
        }
    }

}
