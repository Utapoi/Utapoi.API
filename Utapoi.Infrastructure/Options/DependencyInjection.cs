using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Utapoi.Infrastructure.Options.Admin;
using Utapoi.Infrastructure.Options.JWT;
using Utapoi.Infrastructure.Options.Server;

namespace Utapoi.Infrastructure.Options;

public static class DependencyInjection
{
    // Note(Mikyan): Avoid name clash with default AddOptions.
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration _)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration($"SecurityOptions:{nameof(JwtOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ServerOptions>()
            .BindConfiguration($"{nameof(ServerOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<AdminOptions>()
            .BindConfiguration($"SecurityOptions:{nameof(AdminOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return services;
    }
}