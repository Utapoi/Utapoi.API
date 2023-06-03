using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Persistence.Contexts;

/// <summary>
///     The stats database context.
/// </summary>
public class StatsDbContext : DbContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StatsDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    public StatsDbContext(DbContextOptions<StatsDbContext> options) : base(options)
    {
    }
}