using Microsoft.EntityFrameworkCore;
using RpgGame.Database.Tables;

namespace RpgGame.Database;

public sealed class RpgGameDatabase: DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<DailyRewardCollected> DailyRewardsCollected => Set<DailyRewardCollected>();

    public RpgGameDatabase(DbContextOptions<RpgGameDatabase> options) : base(options)
    {

    }
}