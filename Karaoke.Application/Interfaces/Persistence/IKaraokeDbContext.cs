using Karaoke.Core.Entities.Songs;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Interfaces.Persistence;

public interface IKaraokeDbContext
{
    DbSet<Song> Songs { get; }
}