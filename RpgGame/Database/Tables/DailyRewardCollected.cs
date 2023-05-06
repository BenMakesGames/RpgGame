namespace RpgGame.Database.Tables;

public sealed class DailyRewardCollected: RpgGameTable
{
    public required long PlayerId { get; set; }
    public Player? Player { get; set; }

    public DateTime CollectedOn { get; set; } = DateTime.UtcNow;
}