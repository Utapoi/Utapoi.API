using Karaoke.Application.Identity.Common;
using Karaoke.Application.Identity.GoogleAuth;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Infrastructure.Identity.Auth;
using Karaoke.Infrastructure.Identity.Entities;
using Karaoke.Infrastructure.Identity.Tokens;
using Karaoke.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karaoke.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>(options => { options.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        
        services.AddOpenIddict()
            .AddCore(x =>
            {
                x.UseEntityFrameworkCore()
                    .UseDbContext<AuthDbContext>();
            })
            .AddServer(x =>
            {
                x.SetAuthorizationEndpointUris("Auth/Authorize");
                x.SetTokenEndpointUris("Auth/Token");
                
                x.AllowAuthorizationCodeFlow();

                x.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
                
                x.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough();
                
                x.RegisterScopes(Scopes.GetAll().ToArray());
            })
            .AddClient(x =>
            {
                x.AllowAuthorizationCodeFlow();

                x.AddDevelopmentEncryptionCertificate()
                 .AddDevelopmentSigningCertificate();

                x.UseAspNetCore()
                 .EnableRedirectionEndpointPassthrough();

                x.UseSystemNetHttp()
                    .SetProductInformation(typeof(DependencyInjection).Assembly);


                x.SetRedirectionEndpointUris(
                    "/Auth/Google/AuthorizeCallback"
                );
                
                x.UseWebProviders()
                    .UseGoogle();
            })
            .AddValidation(x =>
            {
                x.UseLocalServer();
                x.UseAspNetCore();
            });

        services.AddAuthorization()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();

        return services;
    }

    public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}