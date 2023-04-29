namespace PetGame.Database.Tables;

public sealed class Pet: PetGameTable
{
    public Player? Owner { get; set; }
    public long? OwnerId { get; set; }

    public required string Image { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public int Energy { get; set; } = 4;

    public string BackgroundImage { get; set; } = "";
    public string ForegroundImage { get; set; } = "";

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