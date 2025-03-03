using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using TechInvent.BLL.Converters.ComponentsConverter.DtoToDM;
using TechInvent.BLL.Converters.FrontendDto;
using TechInvent.BLL.Converters.InventDto;
using TechInvent.BLL.Dto;
using TechInvent.BLL.Interfaces;
using TechInvent.BLL.Interfaces.Converter;
using TechInvent.BLL.Services;
using TechInvent.DAL.Data;
using TechInvent.DAL.Interfaces;
using TechInvent.DAL.Repositories;
using TechInvent.DM.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddScoped<IConverter<InventCabinetDto, Cabinet>, CabinetDtoToCabinetConverter>();
builder.Services.AddScoped<IConverter<Cabinet, CabinetDto>, CabinetToDtoCabinetConverter>();
builder.Services.AddScoped<IConverter<Cabinet, InventCabinetDto>, CabinetToDtoInventCabinetConverter>();
builder.Services.AddScoped<IConverter<WorkplaceDto, Workplace>, WorkplaceDtoToWorkplaceConverter>();
builder.Services.AddScoped<IConverter<Workplace, WorkplaceDto>, WorkplaceToWorkplaceDtoConverter>();
builder.Services.AddScoped<IConverter<SoftwareDto, Software>, SoftwareDtoToSoftwareConverter>();
builder.Services.AddScoped<IConverter<RamDto, Ram>, RamDtoToRamConverter>();
builder.Services.AddScoped<IConverter<GpuDto, Gpu>, GpuDtoToGpuConverter>();
builder.Services.AddScoped<IConverter<ProcessorDto, Processor>, ProcessorDtoToProcessorConverter>();
builder.Services.AddScoped<IConverter<NetDto, NetAdapter>, NetDtoToNetAdapterConverter>();
builder.Services.AddScoped<IConverter<DiskDto, Disk>, DiskDtoToDiskConverter>();
builder.Services.AddScoped<IConverter<MainboardDto, Mainboard>, MainboardDtoToMainboardConverter>();
builder.Services.AddScoped<IConverter<HardwareInfo, List<Component>>, ComponentsConverter>();
builder.Services.AddScoped<ICabinetRepository, CabinetRepository>();
builder.Services.AddScoped<IWorkplaceRepository, WorkplaceRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddScoped<IAdapterTypeRepository, AdapterTypeRepository>();
builder.Services.AddScoped<IOsRepository, OsRepository>();
builder.Services.AddScoped<ICabinetService, CabinetService>();
builder.Services.AddScoped<IInventService, InventService>();
builder.Services.AddScoped<ISoftwareService, SoftwareService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging => {
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

builder.Services.AddDbContext<TechInventContext>(options =>
{
    options.UseSqlite("DataSource=techinvent.db").EnableSensitiveDataLogging();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
