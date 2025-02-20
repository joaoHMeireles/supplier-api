using Microsoft.EntityFrameworkCore;
using SupplierAPI;
using SupplierAPI.Data;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.UseRouting();

app.MapControllers();

// app.UsePathBase("/api");

app.Run();