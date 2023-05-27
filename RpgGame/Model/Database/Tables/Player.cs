namespace RpgGame.Model.Database.Tables;

public sealed class Player: RpgGameTable
{
    public required string EmailAddress { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public DateTimeOffset SignUpDate { get; set; } = DateTimeOffset.UtcNow;
}