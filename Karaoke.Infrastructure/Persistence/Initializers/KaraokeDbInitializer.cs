using Karaoke.Application.Persistence;
using Karaoke.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Karaoke.Infrastructure.Persistence.Initializers;

public class KaraokeDbInitializer : IInitializer
{
    private readonly KaraokeDbContext _context;

    private readonly ILogger<KaraokeDbInitializer> _logger;

    public KaraokeDbInitializer(KaraokeDbContext context, ILogger<KaraokeDbInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        try
        {
            await TrySeedAsync(configuration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public Task TrySeedAsync(IConfiguration configuration)
    {
        return Task.CompletedTask;
    }
}