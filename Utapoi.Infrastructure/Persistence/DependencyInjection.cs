﻿using Microsoft.EntityFrameworkCore;
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

        AddAuthDbContext(services, configuration);
        AddKaraokeDbContext(services, configuration);
        AddStatsDbContext(services, configuration);

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
        var r = configuration.GetConnectionString("KaraokeDb");

        services.AddDbContext<KaraokeDbContext>(x =>
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("KaraokeDb");
            }
            else if (configuration.GetValue<bool>("UseSQLiteDatabase"))
            {
                x.UseMongoDB(
                    new MongoClient(configuration.GetConnectionString("KaraokeDb")),
                    "utapoi"
                );
                //x.UseSqlite(configuration.GetConnectionString("KaraokeDb"),
                //    builder => { builder.MigrationsAssembly(typeof(KaraokeDbContext).Assembly.FullName); });
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

        services.AddScoped<IInitializer, KaraokeDbInitializer>();
        services.AddScoped<IKaraokeDbContext>(provider => provider.GetRequiredService<KaraokeDbContext>());
    }

    /// <summary>
    ///     Adds and configures the authentication database context.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" />.
    /// </param>
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    private static void AddAuthDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(x =>
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("AuthDb");
            }
            else if (configuration.GetValue<bool>("UseSQLiteDatabase"))
            {
                x.UseSqlite(configuration.GetConnectionString("AuthDb"),
                    builder => { builder.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName); });
            }
            else
            {
                x.UseSqlServer(configuration.GetConnectionString("AuthDb"),
                    builder => { builder.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName); });
            }

            x.UseOpenIddict();
            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });

        services.AddScoped<IInitializer, AuthDbContextInitializer>();
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
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                // Note(Mikyan): Switch to SQLite or MSSQL for testing?
                x.UseInMemoryDatabase("StatsDb");
            }
            else if (configuration.GetValue<bool>("UseSQLiteDatabase"))
            {
                x.UseSqlite(configuration.GetConnectionString("StatsDb"),
                    builder => { builder.MigrationsAssembly(typeof(StatsDbContext).Assembly.FullName); });
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