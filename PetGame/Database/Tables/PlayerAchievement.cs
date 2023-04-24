namespace PetGame.Database.Tables;

public sealed class PlayerAchievement: PetGameTable
{
    public required long PlayerId { get; set; }
    public Player? Player { get; set; }
    
    public required AchievementType Achievement { get; set; }
}

public enum AchievementType
{
    Fed10Times,
    Fed100Times,
    Fed1000Times,
}