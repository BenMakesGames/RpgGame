using Microsoft.EntityFrameworkCore;
using RpgGame.Model;
using RpgGame.Model.Database;
using RpgGame.Model.Database.Tables;

namespace RpgGame.Functions;

public static class InventoryHelpers
{
    public static async Task CollectItemAsync(RpgGameDatabase db, long playerId, ItemType item, long quantity)
    {
        var stack = await db.PlayerInventories
            .FirstOrDefaultAsync(i => i.OwnerId == playerId && i.Type == item);

        if(stack == null)
        {
            var newStack = new PlayerInventory()
            {
                OwnerId = playerId,
                Type = item,
                Quantity = quantity,
            };

            db.Add(newStack);
        }
        else
        {
            stack.Quantity += quantity;
        }
    }
}