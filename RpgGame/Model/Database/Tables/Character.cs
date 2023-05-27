namespace RpgGame.Model.Database.Tables;

public sealed class Character: RpgGameTable
{
    public const int StartingEnergy = 4;
    public const int MaximumEnergy = 10;
    public const int StartingLevel = 0;
    
    public Player? Owner { get; set; }
    public long? OwnerId { get; set; }

    public required string Image { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; } = StartingLevel;
    public int Experience { get; set; }

    public int Energy { get; set; } = StartingEnergy;

    public void GainExperience(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative!");

        Experience += amount;

        while (Experience >= ExperienceToLevelUp())
        {
            Experience -= ExperienceToLevelUp();
            Level++;
        }
    }
    
    public int ExperienceToLevelUp() => (Level + 1) * 10;
}