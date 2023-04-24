namespace PetGame.Database.Tables;

public sealed class PlayerStat: PetGameTable
{
    public required long PlayerId { get; set; }
    public Player? Player { get; set; }

    public required StatType Stat { get; set; }
    public required long Value { get; set; }
}

public enum StatType
{
    FedAPet,
}
