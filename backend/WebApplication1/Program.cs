using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using WebApplication1.Controllers.Sensors.Mapping;
using WebApplication1.Data.Sensors;
using WebApplication1.Data.Sensors.Repos;
using WebApplication1.Services.Senders;
using WebApplication1.Services.Sensors;
using WebApplication1.Services.Sensors.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    }
);

// cors
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000", "http://localhost:5100")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowCredentials());
});*/
builder.Services.AddCors(options => options.AddPolicy(
     "AllowAll",
     p => p.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()));

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new SensorDataServiceProfile());
    cfg.AddProfile(new SensorDataControllerProfile());
});

// Dependency Injection Repos
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();

// Dependency Injection Services
builder.Services.AddScoped<ISensorDataService, SensorDataService>();


// BackgroundServices as sensors
builder.Services.AddHostedService<SensorsBackgroundService>();

// DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Register the DbContext service
builder.Services.AddDbContext<SensorDataDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5100/");
});

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    var kestrelSection = context.Configuration.GetSection("Kestrel");

    serverOptions.Configure(kestrelSection)
        .Endpoint("HTTP", listenOptions =>
        {
            // ...
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
