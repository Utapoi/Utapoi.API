using Microsoft.Extensions.Options;
using OpenIddict.Client.WebIntegration;

namespace Utapoi.Infrastructure.Options.Google;

public class ConfigureGoogleOptions : IConfigureNamedOptions<OpenIddictClientWebIntegrationOptions.Google>
{
    private readonly GoogleAuthOptions _googleAuthOptions;

    public ConfigureGoogleOptions(IOptions<GoogleAuthOptions> googleAuthOptions)
    {
        _googleAuthOptions = googleAuthOptions.Value;
    }

    public void Configure(OpenIddictClientWebIntegrationOptions.Google options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, OpenIddictClientWebIntegrationOptions.Google options)
    {
        options.ClientId = _googleAuthOptions.ClientId;
        options.ClientSecret = _googleAuthOptions.ClientSecret;

        options.Scopes.Add("profile");
        
        foreach (var scope in _googleAuthOptions.Scopes)
        {
            options.Scopes.Add(scope);
        }

        options.RedirectUri = new Uri("Auth/Google/AuthorizeCallback", UriKind.Relative);
    }
}