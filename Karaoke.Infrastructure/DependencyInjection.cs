using Karaoke.Application.Identity.Auth;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Application.Interfaces.Persistence;
using Karaoke.Infrastructure.Identity;
using Karaoke.Infrastructure.Identity.Auth;
using Karaoke.Infrastructure.Identity.JWT;
using Karaoke.Infrastructure.Identity.Tokens;
using Karaoke.Infrastructure.Persistence;
using Karaoke.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    /// <param name="configuration">
    ///     The <see cref="IConfiguration" />.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        AddAuthDbContext(services, configuration);
        AddKaraokeDbContext(services, configuration);
        AddStatsDbContext(services, configuration);

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
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
        services.AddDbContext<KaraokeDbContext>(x =>
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
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
            else
            {
                x.UseSqlServer(configuration.GetConnectionString("AuthDb"),
                    builder => { builder.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName); });
            }

            x.EnableSensitiveDataLogging();
            x.EnableDetailedErrors();
            x.LogTo(Console.WriteLine);
        });

        services.AddScoped<AuthDbContextInitializer>();

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
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