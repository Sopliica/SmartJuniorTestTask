using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;
using SmartJuniorTestTask.Repos;
using SmartJuniorTestTask.Repos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MsSql");
builder.Services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IProductionFacilityRepository, ProductionFacilityRepository>();
builder.Services.AddScoped<IEquipmentPlacemntContractRepository, EquipmentPlacementContractRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.MapGet("/", () => "Hello World!");
app.UseSwaggerUI();

app.Run();
