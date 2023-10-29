using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Utapoi.Application.Persistence;
using Utapoi.Infrastructure.Persistence.Contexts;
using Utapoi.Infrastructure.Persistence.Initializers;
using Utapoi.Infrastructure.Persistence.Interceptors;

namespace Utapoi.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        AddKaraokeDbContext(services, configuration);
        // AddStatsDbContext(services, configuration);

        return services;
    }

    /// <summary>
    ///     Adds and configures the karaoke database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddKaraokeDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UtapoiDbContext>(x =>
        {
            x.UseMongoDB(
                new MongoClient(configuration.GetConnectionString("UtapoiDb")),
                "Utapoi"
            );
            
            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });

        services.AddScoped<IInitializer, UtapoiDbInitializer>();
        services.AddScoped<IUtapoiDbContext>(provider => provider.GetRequiredService<UtapoiDbContext>());
    }

    /// <summary>
    ///     Adds and configures the stats database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddStatsDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StatsDbContext>(x =>
        {
            x.UseMongoDB(
                new MongoClient(configuration.GetConnectionString("StatsDb")),
                "UtapoiStats"
            );

            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });
    }
}