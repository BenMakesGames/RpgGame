using Microsoft.EntityFrameworkCore;
using RpgGame.Database.Tables;

namespace RpgGame.Database;

public sealed class RpgGameDatabase: DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Plant> Plants => Set<Plant>();

    public RpgGameDatabase(DbContextOptions<RpgGameDatabase> options) : base(options)
    {

    }
}