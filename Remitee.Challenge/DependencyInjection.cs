using Mapster;
using Microsoft.AspNetCore.ResponseCompression;
using Remitee.Challenge.Application.Common.Extensions;
using Remitee.Challenge.Application.Common.Mappings;

namespace Remitee.Challenge.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            // Mapster
            services.AddMapsterServices();
            MappingConfig.RegisterMappings(TypeAdapterConfig.GlobalSettings);

            // Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });

            // Compresión
            services.AddResponseCompression(options =>
            {
                var mimeTypes = new List<string>
                {
                    "text/plain",
                    "application/json"
                };

                options.EnableForHttps = true;
                options.MimeTypes = mimeTypes;
                options.Providers.Add<GzipCompressionProvider>();
            });

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("_configCors", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(origin => true)
                          .AllowCredentials();
                });
            });

            // MediatR (apunta a la capa Application)
            var applicationAssembly = typeof(MappingConfig).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            return services;
        }
    }
}
