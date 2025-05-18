using TechInvent.BLL.DtoModels;
using TechInvent.BLL.Service;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Dto
{
    public class DtoConverter
    {
        private readonly EntityCheckerService _entityChecker;

        public DtoConverter(EntityCheckerService entityChecker)
        {
            _entityChecker = entityChecker;
        }

        public async Task<Cabinet> ConvertDtoCabinetAsync(CabinetDto cabinetDto)
        {
            Cabinet cabinet = await _entityChecker.GetOrCreateCabinetAsync(cabinetDto.Name);
            await ConvertDtoWorkPlaceAsync(cabinetDto.Workplace, cabinet);
            await _entityChecker.SaveChangesAsync();
            return cabinet;
        }

        public async Task<Workplace> ConvertDtoWorkPlaceAsync(WorkplaceDto workplaceDto, Cabinet cabinet)
        {
            var os = await _entityChecker.GetOrCreateOsAsync(workplaceDto.OsName, workplaceDto.Version);
            List<Component> components = await ConvertDtoComponentsAsync(workplaceDto.HardwareInfo);
            List<Software> software = await ConvertDtoSoftwareAsync(workplaceDto.Software);

            var workplace = await _entityChecker.GetOrCreateWorkplaceAsync(workplaceDto.CompName, cabinet, os, components, software);
            return workplace;
        }

        public async Task<List<Software>> ConvertDtoSoftwareAsync(List<SoftwareDto> softwareDtoList)
        {
            var softwareList = new List<Software>();
            foreach (SoftwareDto softwareDto in softwareDtoList)
            {
                var manufacturer = await _entityChecker.GetOrCreateManufacturerAsync(softwareDto.Vendor);
                var software = await _entityChecker.GetOrCreateSoftwareAsync(softwareDto.Name, softwareDto.Version, manufacturer.IdManufacturer);
                softwareList.Add(software);
            }
            return softwareList;
        }

        public async Task<List<Component>> ConvertDtoComponentsAsync(HardwareInfo hardwareInfo)
        {
            var components = new List<Component>();

            foreach (var ramDto in hardwareInfo.Ram)
                components.Add(await ConvertDtoRamAsync(ramDto));
            foreach (var gpuDto in hardwareInfo.Gpu)
                components.Add(ConvertDtoGpu(gpuDto));
            foreach (var processorDto in hardwareInfo.Processor)
                components.Add(ConvertDtoProcessor(processorDto));
            foreach (var netDto in hardwareInfo.Net)
                components.Add(await ConvertDtoNetAdapterAsync(netDto));
            foreach (var diskDto in hardwareInfo.Disks)
                components.Add(ConvertDtoDisk(diskDto));

            components.Add(ConvertDtoMainboard(hardwareInfo.Mainboard));
            return components;
        }

        public async Task<Ram> ConvertDtoRamAsync(RamDto ramDto)
        {
            var manufacturer = await _entityChecker.GetOrCreateManufacturerAsync(ramDto.Manufacturer);
            return new Ram
            {
                Name = ramDto.Name,
                Speed = ramDto.Speed.ToString(),
                Capacity = ramDto.Capacity,
                MemoryType = ramDto.MemoryType.ToString(),
                PartNumber = ramDto.PartNumber,
                SerialNumber = ramDto.SerialNumber,
                IdManufacturer = manufacturer.IdManufacturer,
                IdManufacturerNavigation = manufacturer,
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

        public Disk ConvertDtoDisk(DiskDto diskDto)
        {
            return new Disk
            {
                Name = diskDto.Model,
                Size = diskDto.Size
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

        public async Task<NetAdapter> ConvertDtoNetAdapterAsync(NetDto netDto)
        {
            var manufacturer = await _entityChecker.GetOrCreateManufacturerAsync(netDto.Manufacturer);
            var adapterType = await _entityChecker.GetOrCreateAdapterTypeAsync(netDto.AdapterType);
            return new NetAdapter
            {
                Name = netDto.Name,
                MacAddress = netDto.MacAddress,
                IdManufacturer = manufacturer.IdManufacturer,
                IdManufacturerNavigation = manufacturer,
                IdAdapterType = adapterType.IdAdapterType,
                AdapterTypeNavigation = adapterType
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
