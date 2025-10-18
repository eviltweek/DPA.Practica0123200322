using DPA.Practica0123200322.CORE.Core.Interfaces;
using DPA.Practica0123200322.CORE.Infrastructure.Data;
using DPA.Practica0123200322.CORE.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _configuration = builder.Configuration;
var _connectionString = _configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UniversidadContext>(options =>
{
    options.UseSqlServer(_connectionString);
});

builder.Services.AddTransient<IEstudianteRepository, EstudianteRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
