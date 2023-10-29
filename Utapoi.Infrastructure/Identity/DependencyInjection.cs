using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Utapoi.Application.Identity.Common;
using Utapoi.Application.Identity.GoogleAuth;
using Utapoi.Application.Identity.Tokens;
using Utapoi.Infrastructure.Identity.Auth;
using Utapoi.Infrastructure.Identity.Entities;
using Utapoi.Infrastructure.Identity.Tokens;
using Utapoi.Infrastructure.Persistence.Contexts;

namespace Utapoi.Infrastructure.Identity;

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