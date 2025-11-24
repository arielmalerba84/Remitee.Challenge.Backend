using Mapster;
using Microsoft.AspNetCore.ResponseCompression;
using Remitee.Challenge.Application.Common.Extensions;
using Remitee.Challenge.Application.Common.Mappings;

namespace Remitee.Challenge.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            // -----------------------------------------------------
            // MAPSTER – Mapeos automáticos
            // -----------------------------------------------------
            services.AddMapsterServices();
            MappingConfig.RegisterMappings(TypeAdapterConfig.GlobalSettings);

            // -----------------------------------------------------
            // LOGGING
            // -----------------------------------------------------
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            // -----------------------------------------------------
            // COMPRESIÓN GZIP
            // -----------------------------------------------------
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;

                options.MimeTypes = new[]
                {
                    "text/plain",
                    "application/json",
                    "application/xml",
                    "text/css",
                    "text/html",
                };

                options.Providers.Add<GzipCompressionProvider>();
            });

            // -----------------------------------------------------
            // CORS
            // -----------------------------------------------------
            services.AddCors(options =>
            {
                options.AddPolicy("_configCors", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

           

            return services;
        }
    }
}
