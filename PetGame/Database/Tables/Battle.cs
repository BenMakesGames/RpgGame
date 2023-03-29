namespace PetGame.Database.Tables;

public sealed class Battle: PetGameTable
{
    public required long PlayerId { get; set; }
    public Player? Player { get; set; }
    
    public required long PetId { get; set; }
    public Pet? Pet { get; set; }
    
    public required string MonsterName { get; set; }
    public required int MonsterMaxHealth { get; set; }
    public required int MonsterHealth { get; set; }
    
    public required int MonsterAttackDice { get; set; }
    public required int MonsterAttackDiceSides { get; set; }
    public required int MonsterAttackBonus { get; set; }
}