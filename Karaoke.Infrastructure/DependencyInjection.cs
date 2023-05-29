using Karaoke.Infrastructure.Identity;
using Karaoke.Infrastructure.Persistence;
using Karaoke.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Karaoke.Infrastructure;

/// <summary>
///     Extension methods for setting up infrastructure related services in an <see cref="IServiceCollection" />.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds and configures infrastructure related services in an <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="env">
    ///     The <see cref="IHostEnvironment" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostEnvironment env,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        AddKaraokeDbContext(services, env, configuration);
        AddAuthDbContext(services, env, configuration);
        AddStatsDbContext(services, env, configuration);

        return services;
    }

    /// <summary>
    ///     Adds and configures the karaoke database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="env">
    ///     The <see cref="IHostEnvironment" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddKaraokeDbContext(IServiceCollection services, IHostEnvironment env,
        IConfiguration configuration)
    {
        services.AddDbContext<KaraokeDbContext>(x =>
        {
            if (env.IsDevelopment())
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("KaraokeDb");
            }
            else
            {
                x.UseSqlServer(configuration.GetConnectionString("KaraokeDb"),
                    builder => { builder.MigrationsAssembly(typeof(KaraokeDbContext).Assembly.FullName); });
            }

            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });
    }

    /// <summary>
    ///     Adds and configures the authentication database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="env">
    ///     The <see cref="IHostEnvironment" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddAuthDbContext(IServiceCollection services, IHostEnvironment env,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(x =>
        {
            if (env.IsDevelopment())
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("AuthDb");
            }
            else
            {
                x.UseSqlServer(configuration.GetConnectionString("AuthDb"),
                    builder => { builder.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName); });
            }

            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });

        services
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AuthDbContext>();
    }

    /// <summary>
    ///     Adds and configures the stats database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="env">
    ///     The <see cref="IHostEnvironment" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddStatsDbContext(IServiceCollection services, IHostEnvironment env,
        IConfiguration configuration)
    {
        services.AddDbContext<StatsDbContext>(x =>
        {
            if (env.IsDevelopment())
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("StatsDb");
            }
            else
            {
                x.UseSqlServer(configuration.GetConnectionString("StatsDb"),
                    builder => { builder.MigrationsAssembly(typeof(StatsDbContext).Assembly.FullName); });
            }

            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });
    }
}