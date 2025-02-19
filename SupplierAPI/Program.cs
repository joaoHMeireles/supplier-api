using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupplierAPI;
using SupplierAPI.Models.Database;
using SupplierAPI.Repositories;
using SupplierAPI.Repositories.Interfaces;
using SupplierAPI.Services;
using SupplierAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SupplierApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseRouting();

app.MapControllers();

// app.UsePathBase("/api");

app.Run();