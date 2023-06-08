using Microsoft.EntityFrameworkCore;
using RpgGame.Database.Tables;

namespace RpgGame.Database;

public sealed class RpgGameDatabase: DbContext
{
    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Player> Players => Set<Player>();

    public RpgGameDatabase(DbContextOptions<RpgGameDatabase> options) : base(options)
    {

    }
}