﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PetGame.Database;

namespace PetGame.Services;

public sealed class CurrentPlayer: AuthenticationStateProvider
{
    public CurrentPlayerData? Info { get; private set; }

    private PetGameDatabase Database { get; }

    public CurrentPlayer(PetGameDatabase database)
    {
        Database = database;
    }

    public async Task<LogInResult> LogInAsync(string emailAddress, string password, CancellationToken ct = default)
    {
        var player = await Database.Players.FirstOrDefaultAsync(p => p.EmailAddress == emailAddress, ct);

        if(player == null || !Security.PasswordsMatch(password, player.Password))
            return new LogInResult(false, "Email address and/or password is not correct.");
        
        // rehash password, if needed:
        if(Security.PasswordNeedsRehash(player.Password))
        {
            player.Password = Security.HashPassword(password);

            await Database.SaveChangesAsync(ct);
        }
        
        Info = new CurrentPlayerData(player.Id, player.Name);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        
        return new(true, "Successfully logged in.");
    }
    
    public async Task LogOutAsync()
    {
        Info = null;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
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
            }, "PetGameAuth");
        
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
    
    public sealed record CurrentPlayerData(long Id, string Name);
    public sealed record LogInResult(bool Success, string Message);
}

