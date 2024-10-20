using TechInventAPI.Data;
using TechInventAPI.Dto;
using TechInventAPI.Service;
using TechInventAPI.RabbitMQ;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHostedService<RabbitMqListener>();
builder.Services.AddDbContext<TechInventContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnectionMysql") ?? "server=;user=;password=;database=;");
});
builder.Services.AddScoped<EntityCheckerService>();
builder.Services.AddScoped<DtoConverter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
