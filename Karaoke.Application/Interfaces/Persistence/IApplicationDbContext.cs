using Karaoke.Core.Entities.Songs;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<Song> Songs { get; }
}