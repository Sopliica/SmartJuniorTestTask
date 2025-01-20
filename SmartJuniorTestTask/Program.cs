using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;
using SmartJuniorTestTask.Repos;
using SmartJuniorTestTask.Repos.Interfaces;
using MediatR;
using SmartJuniorTestTask.Infrastructure.Behaviors;
using FluentValidation;

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

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    await CreateDB(app);    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

async Task CreateDB(IHost host)
{
    await using var scope = host.Services.CreateAsyncScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MsSqlDbContext>();
    await Seeder.Seed(context);
}