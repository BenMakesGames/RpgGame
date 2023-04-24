using Microsoft.EntityFrameworkCore;
using PetGame.Database;
using PetGame.Database.Tables;
using PetGame.Dialogs;

namespace PetGame.Functions;

public sealed class PetFeeding
{
    public static async Task<(string Title, string Message)> Feed(PetGameDatabase db, long playerId, long petId)
    {
        var pet = await db.Pets.FirstOrDefaultAsync(x => x.Id == petId && x.OwnerId == playerId);
        
        if(pet == null)
        {
            return ("Oops!", "Pet not found!");
        }

        if(pet.Energy >= 10)
        {
            return ("Oops!", $"{pet.Name} is already at max energy.");
        }

        pet.Energy = Math.Clamp(pet.Energy + 3, 0, 10);

        var earnedAchievements = await StatHelper.IncreaseStat(db, playerId, StatType.FedAPet, 1);

        if(!earnedAchievements.Any())
            return ("Omnomnom!", $"{pet.Name} is now at {pet.Energy} energy.");

        var listOfAchievements = string.Join(", ", earnedAchievements);
        
        return (
            "Woo!",
            $"{pet.Name} is now at {pet.Energy} energy. Oh! And you earned the following achievements: {listOfAchievements}!"
        );
    }
}