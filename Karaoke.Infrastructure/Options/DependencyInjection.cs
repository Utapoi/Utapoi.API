using Karaoke.Infrastructure.Options.Google;
using Karaoke.Infrastructure.Options.JWT;
using Karaoke.Infrastructure.Options.Server;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Karaoke.Infrastructure.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration($"SecurityOptions:{nameof(JwtOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<GoogleAuthOptions>()
            .BindConfiguration($"GoogleOptions:{nameof(GoogleAuthOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ServerOptions>()
            .BindConfiguration($"{nameof(ServerOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        services.AddSingleton<IConfigureOptions<GoogleOptions>, ConfigureGoogleOptions>();

        return services;
    }
}