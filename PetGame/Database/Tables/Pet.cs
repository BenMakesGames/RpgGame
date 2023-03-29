namespace PetGame.Database.Tables;

public sealed class Pet: PetGameTable
{
    public const int BaseHealth = 10;
    
    public Player? Owner { get; set; }
    public long? OwnerId { get; set; }

    public required string Image { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public int MaxHealth => BaseHealth + Level;
    public int Health { get; set; } = BaseHealth;

    public int MinDamage => Level + 2;
    public int MaxDamage => Level + 5;

    public void GainExperience(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative!");

        Experience += amount;

        while (Experience >= ExperienceToLevelUp())
        {
            Experience -= ExperienceToLevelUp();
            Level++;
            Health = MaxHealth;
        }
    }
    
    public int ExperienceToLevelUp() => (Level + 1) * 10;
}