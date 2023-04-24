using Microsoft.EntityFrameworkCore;
using PetGame.Database;
using PetGame.Database.Tables;

namespace PetGame.Functions;

public sealed class StatHelper
{
    public static async Task<List<AchievementType>> IncreaseStat(PetGameDatabase db, long playerId, StatType stat, long amount)
    {
        var playerStat = await db.PlayerStats.FirstOrDefaultAsync(x => x.PlayerId == playerId && x.Stat == stat);

        var oldValue = playerStat == null ? 0 : playerStat.Value;
        
        if(playerStat == null)
        {
            playerStat = new PlayerStat
            {
                PlayerId = playerId,
                Stat = stat,
                Value = amount
            };
            
            db.Add(playerStat);
        }
        else
        {
            playerStat.Value += amount;
        }

        return await CheckForAchievements(db, playerId, stat, oldValue, playerStat.Value);
    }
    
    public static async Task<List<AchievementType>> CheckForAchievements(PetGameDatabase db, long playerId, StatType stat, long oldValue, long newValue)
    {
        var achievementsEarned = new List<AchievementType>();
        
        if(stat == StatType.FedAPet)
        {
            if(oldValue < 10 && newValue >= 10)
            {
                if(await GetAchievement(db, playerId, AchievementType.Fed10Times))
                    achievementsEarned.Add(AchievementType.Fed10Times);
            }

            if(oldValue < 100 && newValue >= 100)
            {
                if(await GetAchievement(db, playerId, AchievementType.Fed100Times))
                    achievementsEarned.Add(AchievementType.Fed100Times);
            }
            
            if(oldValue < 1000 && newValue >= 1000)
            {
                if(await GetAchievement(db, playerId, AchievementType.Fed1000Times))
                    achievementsEarned.Add(AchievementType.Fed1000Times);
            }
        }

        return achievementsEarned;
    }
    
    public static async Task<bool> GetAchievement(PetGameDatabase db, long playerId, AchievementType achievement)
    {
        var playerAchievement = await db.PlayerAchievements
            .FirstOrDefaultAsync(x => x.PlayerId == playerId && x.Achievement == achievement);

        if(playerAchievement != null)
            return false;

        playerAchievement = new PlayerAchievement
        {
            PlayerId = playerId,
            Achievement = achievement
        };
            
        db.Add(playerAchievement);

        return true;
    }
}