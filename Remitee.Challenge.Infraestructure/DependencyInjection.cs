using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Remitee.Challenge.Domain.Interface;
using Remitee.Challenge.Infraestructure.Common;
using Remitee.Challenge.Infraestructure.Data;
using Remitee.Challenge.Infraestructure.Repositories;


namespace Remitee.Challenge.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PrimaryDbConnection");
            var commandTimeout = configuration.GetValue<int>("CommandTimeout");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString,
                     sqlServerOptions =>
                     {
                         sqlServerOptions.CommandTimeout(commandTimeout);
                         sqlServerOptions.EnableRetryOnFailure(
                             maxRetryCount: 5,
                             maxRetryDelay: TimeSpan.FromSeconds(10),
                             errorNumbersToAdd: null
                         );
                     })
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);
            });

            //Base
            services.AddSingleton(TimeProvider.System);
            services.AddScoped<AppDbContext, ApplicationDbContext>();

            //Repositories
            services.AddScoped<IBookRepository,BookRepository>();

            return services;
        }
    }
}
