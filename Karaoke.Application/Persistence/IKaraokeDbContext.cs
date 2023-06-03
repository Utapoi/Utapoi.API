using Karaoke.Core.Entities.Songs;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Persistence;

public interface IKaraokeDbContext
{
    DbSet<Song> Songs { get; }
}