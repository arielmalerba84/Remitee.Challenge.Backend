using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Remitee.Challenge.Application.Common.Mappings;

namespace Remitee.Challenge.Application.Common.Extensions
{
    public static class MapsterExtensions
    {
        public static void AddMapsterServices(this IServiceCollection services)
        {
            // Registro de Mapster
            var config = TypeAdapterConfig.GlobalSettings;
            MappingConfig.RegisterMappings(config); // clase con los mapeos
            services.AddSingleton(config);

            // Aquí ServiceMapper viene de MapsterMapper
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
