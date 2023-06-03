using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Karaoke.Infrastructure.Options.Google;

public class ConfigureGoogleOptions : IConfigureNamedOptions<GoogleOptions>
{
    private readonly GoogleAuthOptions _googleAuthOptions;

    public ConfigureGoogleOptions(IOptions<GoogleAuthOptions> googleAuthOptions)
    {
        _googleAuthOptions = googleAuthOptions.Value;
    }

    public void Configure(GoogleOptions options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, GoogleOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme && name != GoogleDefaults.AuthenticationScheme)
        {
            return;
        }

        options.ClientId = _googleAuthOptions.ClientId;
        options.ClientSecret = _googleAuthOptions.ClientSecret;

        options.Scope.Add("profile");
        options.SignInScheme = IdentityConstants.ExternalScheme;

        options.AuthorizationEndpoint += "?prompt=consent";

        foreach (var scope in _googleAuthOptions.Scopes)
        {
            if (!options.Scope.Contains(scope))
            {
                options.Scope.Add(scope);
            }
        }
    }
}