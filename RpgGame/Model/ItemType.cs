namespace RpgGame.Model;

public enum ItemType
{
    // raw materials
    Meat = 0,
    Wood = 1,
    Iron = 2,
    
    // crafted
    Sword = 3,
    GrilledMeat = 4,
}

public static class ItemTypeExtensions
{
    public static string GetName(this ItemType type)
    {
        return type switch
        {
            ItemType.Meat => "Meat",
            ItemType.Wood => "Ash",
            ItemType.Iron => "Metal",
            ItemType.Sword => "Sword",
            ItemType.GrilledMeat => "Grilled Meat",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    
    public static string GetImage(this ItemType type)
    {
        return "/images/items/" + type switch
        {
            ItemType.Meat => "steak.svg",
            ItemType.Wood => "log.svg",
            ItemType.Iron => "iron.svg",
            ItemType.Sword => "sword.svg",
            ItemType.GrilledMeat => "grilled-meat.svg",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}