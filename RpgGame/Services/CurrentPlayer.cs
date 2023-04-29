using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using RpgGame.Database;
using RpgGame.Functions;

namespace RpgGame.Services;

/// <summary>
/// This class is used internally by Blazor to determine whether or not the player should have access to certain pages.
/// It is also useful for getting the Id of the current player. For example, the `MyHouse.razor` page loads the current
/// player's characters by getting the player's Id from this service, and then looking up all characters whose OwnerId
/// matches the player's Id.
/// </summary>
public sealed class CurrentPlayer: AuthenticationStateProvider
{
    public CurrentPlayerData? Info { get; private set; }

    private IDbContextFactory<RpgGameDatabase> DbFactory { get; }

    public CurrentPlayer(IDbContextFactory<RpgGameDatabase> dbFactory)
    {
        DbFactory = dbFactory;
    }

    public async Task<LogInResult> LogInAsync(string emailAddress, string password, CancellationToken ct = default)
    {
        await using var database = await DbFactory.CreateDbContextAsync(ct);
        
        var player = await database.Players.FirstOrDefaultAsync(p => p.EmailAddress == emailAddress, ct);

        if(player == null || !Security.PasswordsMatch(password, player.Password))
            return new LogInResult(false, "Email address and/or password is not correct.");
        
        // rehash password, if needed:
        if(Security.PasswordNeedsRehash(player.Password))
        {
            player.Password = Security.HashPassword(password);

            await database.SaveChangesAsync(ct);
        }
        
        Info = new CurrentPlayerData(player.Id, player.Name);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        
        return new(true, "Successfully logged in.");
    }
    
    public Task LogOutAsync()
    {
        Info = null;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        
        return Task.CompletedTask;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if(Info == null)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            return Task.FromResult(new AuthenticationState(user));
        }
        else
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Info?.Id.ToString() ?? ""),
            }, "RpgGameAuth");
        
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
    
    public sealed record CurrentPlayerData(long Id, string Name);
    public sealed record LogInResult(bool Success, string Message);
}

