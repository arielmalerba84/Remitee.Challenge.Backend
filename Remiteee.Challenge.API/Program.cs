using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Remitee.Challenge.API;
using Remitee.Challenge.Application;
using Remitee.Challenge.Application.Common.Middleware;
using Remitee.Challenge.Infraestructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// CONFIGURACIÓN DE LOGGING (SERILOG)
// -----------------------------------------------------
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
);

// -----------------------------------------------------
//  SERVICIOS WEB
// -----------------------------------------------------
builder.Services.AddControllers();

// -----------------------------------------------------
//  SWAGGER
// -----------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Remitee Challenge",
        Version = "v1",
        Description = "Documentación de la API Remitee Challenge"
    });

    // Permite usar los atributos [SwaggerOperation] y similares
    c.EnableAnnotations();
});

// -----------------------------------------------------
// CAPAS APPLICATION E INFRASTRUCTURE
// -----------------------------------------------------
builder.Services.AddApplicationServices();                 // Validators, MediatR, pipeline
builder.Services.AddInfrastructureServices(builder.Configuration);  // DB, repos, etc.
builder.Services.AddWebServices(builder.Configuration);    // Mapster, CORS, compresión, logging

var app = builder.Build();

// -----------------------------------------------------
// MIDDLEWARE
// -----------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Remitee Challenge V1");
        c.RoutePrefix = "swagger";
    });
}


app.UseCors("_configCors");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();
