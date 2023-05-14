namespace RpgGame.Database.Tables;

public sealed class Plant: RpgGameTable
{
    public required long OwnerId { get; set; }
    public Player? Owner { get; set; }

    public DateTimeOffset PlantedOn { get; set; } = DateTimeOffset.UtcNow;

    public required PlantType Type { get; set; }

    public int PercentGrown(DateTimeOffset now)
    {
        const int eightHoursInSeconds = 8 * 60 * 60;
        
        var growTimeInSeconds = Math.Clamp((now - PlantedOn).TotalSeconds, 0, eightHoursInSeconds);

        return (int)(growTimeInSeconds / eightHoursInSeconds * 100);
    }
}

public enum PlantType
{
    Apple = 0,
    Bananer = 2,
    Money = 1,
}