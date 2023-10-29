using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utapoi.Application.Persistence;
using Utapoi.Infrastructure.Persistence.Contexts;

namespace Utapoi.Infrastructure.Persistence.Initializers;

public class UtapoiDbInitializer : IInitializer
{
    private readonly UtapoiDbContext _context;

    private readonly ILogger<UtapoiDbInitializer> _logger;

    public UtapoiDbInitializer(UtapoiDbContext context, ILogger<UtapoiDbInitializer> logger)
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