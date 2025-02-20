using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SupplierAPI;
using DateTimeConverter = SupplierAPI.Converters.DateTimeConverter;
using SupplierAPI.Data;
using SupplierAPI.Repositories;
using SupplierAPI.Repositories.Interfaces;
using SupplierAPI.Services;
using SupplierAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Suppliers API",
        Description = "CRUD operations in ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Project Github repo",
            Url = new Uri("https://github.com/joaoHMeireles/supplier-api/tree/main")
        },
        License = new OpenApiLicense
        {
            Name = "Creator Linkedin profile",
            Url = new Uri("https://www.linkedin.com/in/joao-henrique-meireles/")
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<SupplierApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapSwagger().RequireAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();