using Karaoke.Application.Identity.GoogleAuth;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Infrastructure.Identity.Auth;
using Karaoke.Infrastructure.Identity.Entities;
using Karaoke.Infrastructure.Identity.Tokens;
using Karaoke.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        //services
        //    .AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer()
        //    .AddCookie()
        //    .AddGoogle();

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
        //app.UseAuthentication();
        //app.UseAuthorization();

        return app;
    }
}