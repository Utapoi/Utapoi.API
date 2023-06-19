using Karaoke.Infrastructure.Options.Admin;
using Karaoke.Infrastructure.Options.Google;
using Karaoke.Infrastructure.Options.JWT;
using Karaoke.Infrastructure.Options.Server;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Client.WebIntegration;

namespace Karaoke.Infrastructure.Options;

public static class DependencyInjection
{
    // Note(Mikyan): Avoid name clash with default AddOptions.
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration _)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration($"SecurityOptions:{nameof(JwtOptions)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<GoogleAuthOptions>()
            .BindConfiguration($"GoogleOptions:Auth")
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
        services.AddSingleton<IConfigureOptions<OpenIddictClientWebIntegrationOptions.Google>, ConfigureGoogleOptions>();

        return services;
    }
}