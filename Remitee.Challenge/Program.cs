using Microsoft.OpenApi.Models;
using Remitee.Challenge.API;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Capa Web Services (incluye CORS, Mapster, Compression, MediatR, Logging)
builder.Services.AddWebServices(builder.Configuration);

// Swagger Oficial
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Remitee API",
        Version = "v1",
        Description = "API Remitee Challenge"
    });
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Remitee API v1");
});

// CORS
app.UseCors("_configCors");

// Controllers
app.MapControllers();

app.Run();
