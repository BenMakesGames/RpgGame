namespace RpgGame.Model.Database.Tables;

public sealed class PlayerInventory: RpgGameTable
{
    public required long OwnerId { get; set; }
    public Player? Owner { get; set; }
    
    public required ItemType Type { get; set; }
    public required long Quantity { get; set; }
}
