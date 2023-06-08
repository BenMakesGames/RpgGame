namespace RpgGame.Database.Tables;

public sealed class Announcement: RpgGameTable
{
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

    public required string Title { get; set; }
    public required string Body { get; set; }
}