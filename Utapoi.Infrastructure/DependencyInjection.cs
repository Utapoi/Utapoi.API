using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utapoi.Application.Albums;
using Utapoi.Application.Files;
using Utapoi.Application.Karaoke;
using Utapoi.Application.LocalizedStrings.Interfaces;
using Utapoi.Application.Lyrics;
using Utapoi.Application.Singers;
using Utapoi.Application.Songs;
using Utapoi.Application.Tags;
using Utapoi.Application.Users.Interfaces;
using Utapoi.Core.Storage;
using Utapoi.Infrastructure.Albums;
using Utapoi.Infrastructure.Files;
using Utapoi.Infrastructure.Identity;
using Utapoi.Infrastructure.Karaoke;
using Utapoi.Infrastructure.LocalizedStrings;
using Utapoi.Infrastructure.Lyrics;
using Utapoi.Infrastructure.Options;
using Utapoi.Infrastructure.Persistence;
using Utapoi.Infrastructure.Singers;
using Utapoi.Infrastructure.Songs;
using Utapoi.Infrastructure.Tags;
using Utapoi.Infrastructure.Users;

namespace Utapoi.Infrastructure;

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
        services
            .AddOptions(configuration)
            // .AddIdentity()
            .AddPersistence(configuration);

        //services.AddScoped<IUsersService, UsersService>();
        services.AddSingleton<Storage, NativeStorage>(_ => new NativeStorage(Directory.GetCurrentDirectory()));

        // TODO: Add Source Generators to register all services
        services.AddScoped<IAlbumsService, AlbumsService>();
        services.AddScoped<IFilesService, FilesService>();
        services.AddScoped<IKaraokeService, KaraokeService>();
        services.AddScoped<ISingersService, SingersService>();
        services.AddScoped<ISongsService, SongsService>();
        services.AddScoped<ITagsService, TagsService>();
        services.AddScoped<ILocalizedStringsService, LocalizedStringsService>();
        services.AddScoped<ILyricsService, LyricsService>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        //app.UseIdentity();

        return app;
    }
}