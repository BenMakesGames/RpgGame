using Microsoft.EntityFrameworkCore;
using RpgGame.Model.Database.Tables;

namespace RpgGame.Model.Database;

public sealed class RpgGameDatabase: DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<PlayerInventory> PlayerInventories => Set<PlayerInventory>();

    public RpgGameDatabase(DbContextOptions<RpgGameDatabase> options) : base(options)
    {

    }
}