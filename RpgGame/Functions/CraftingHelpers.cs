using RpgGame.Model;
using RpgGame.Model.Database.Tables;

namespace RpgGame.Functions;

public static class CraftingHelpers
{
    public static CanMakeResult CanMake(Recipe recipe, IList<PlayerInventory> availableIngredients)
    {
        foreach(var (requiredIngredient, requiredQuantity) in recipe.Ingredients)
        {
            if(!availableIngredients.Any(x => x.Type == requiredIngredient && x.Quantity >= requiredQuantity))
                return new CanMakeResult(false, requiredIngredient, requiredQuantity);
        }

        return CanMakeResult.OK;
    }
    
    public static readonly IReadOnlyDictionary<string, Recipe> Recipes = new Dictionary<string, Recipe>()
    {
        {
            "Sword",
            new Recipe(
                new Dictionary<ItemType, int>() { {ItemType.Wood, 1}, {ItemType.Iron, 2} },
                new Dictionary<ItemType, int>() { {ItemType.Sword, 1} }
            )
        },
        {
            "Grilled Meat",
            new Recipe(
                new Dictionary<ItemType, int>() { {ItemType.Meat, 1}, {ItemType.Wood, 1} },
                new Dictionary<ItemType, int>() { {ItemType.GrilledMeat, 1} }
            )
        },
    };
}

public sealed record Recipe(
    IReadOnlyDictionary<ItemType, int> Ingredients,
    IReadOnlyDictionary<ItemType, int> Results
);

public sealed record CanMakeResult(bool CanMake, ItemType? MissingIngredient, int? QuantityRequired)
{
    public static readonly CanMakeResult OK = new(true, null, null);
}