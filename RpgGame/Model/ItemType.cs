namespace RpgGame.Model;

public enum ItemType
{
    Meat = 0,
    Wood = 1,
    Iron = 2,
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
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}