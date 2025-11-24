using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Remitee.Challenge.Application.Common.Behaviour;
using System.Reflection;

namespace Remitee.Challenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registra todos los validadores de FluentValidation en el assembly actual
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuración de MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}
