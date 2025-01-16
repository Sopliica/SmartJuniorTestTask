using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MsSql");
builder.Services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(connectionString));



var app = builder.Build();
app.UseSwagger();
app.MapGet("/", () => "Hello World!");
app.UseSwaggerUI();

app.Run();
