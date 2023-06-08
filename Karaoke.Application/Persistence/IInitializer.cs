using Microsoft.Extensions.Configuration;

namespace Karaoke.Application.Persistence;

public interface IInitializer
{
    Task InitialiseAsync();

    Task SeedAsync(IConfiguration configuration);

    Task TrySeedAsync(IConfiguration configuration);
}