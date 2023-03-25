namespace PetGame.Database.Tables;

public sealed class Player: PetGameTable
{
    public required string EmailAddress { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public DateTimeOffset SignUpDate { get; set; } = DateTimeOffset.UtcNow;

    public int Money { get; set; }
}