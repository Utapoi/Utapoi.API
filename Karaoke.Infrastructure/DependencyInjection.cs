using Karaoke.Application.Albums;
using Karaoke.Application.Files;
using Karaoke.Application.Karaoke;
using Karaoke.Application.Singers;
using Karaoke.Application.Songs;
using Karaoke.Application.Tags;
using Karaoke.Application.Users.Interfaces;
using Karaoke.Core.Storage;
using Karaoke.Infrastructure.Albums;
using Karaoke.Infrastructure.Files;
using Karaoke.Infrastructure.Identity;
using Karaoke.Infrastructure.Karaoke;
using Karaoke.Infrastructure.Options;
using Karaoke.Infrastructure.Persistence;
using Karaoke.Infrastructure.Singers;
using Karaoke.Infrastructure.Songs;
using Karaoke.Infrastructure.Tags;
using Karaoke.Infrastructure.Users.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddOptions(configuration)
            .AddIdentity(configuration)
            .AddPersistence(configuration);

        services.AddScoped<IUsersService, UsersService>();
        services.AddSingleton<Storage, NativeStorage>(x => new NativeStorage(Directory.GetCurrentDirectory()));

        // TODO: Add Reflection to register all services
        services.AddScoped<IAlbumsService, AlbumsService>();
        services.AddScoped<IFilesService, FilesService>();
        services.AddScoped<IKaraokeService, KaraokeService>();
        services.AddScoped<ISingersService, SingersService>();
        services.AddScoped<ISongsService, SongsService>();
        services.AddScoped<ITagsService, TagsService>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseIdentity();

        return app;
    }
}