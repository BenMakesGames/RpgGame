using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using PetGame.Database;
using PetGame.Functions;

namespace PetGame.Services;

public sealed class RememberMe
{
    private const string LocalStorageKey = "remember-me";
    
    // TODO: before deploying this site for public use, create your own, random 32-byte string:
    private const string EncryptionKey = "rh622a=TCCE_BH{mxJ@79-=w..Wf[GSW";
    
    private ILocalStorageService LocalStorage { get; }
    private IDbContextFactory<PetGameDatabase> DatabaseFactory { get; }
    private CurrentPlayer CurrentPlayer { get; }

    public RememberMe(
        ILocalStorageService localStorage, IDbContextFactory<PetGameDatabase> databaseFactory,
        CurrentPlayer currentPlayer
    )
    {
        LocalStorage = localStorage;
        DatabaseFactory = databaseFactory;
        CurrentPlayer = currentPlayer;
    }

    public async Task LogIn()
    {
        var data = await LocalStorage.GetItemAsync<RememberMeData>(LocalStorageKey);
        
        if (data is null)
            return;

        try
        {
            var password = Security.Decrypt(data.Password, EncryptionKey);
        
            await CurrentPlayer.LogInAsync(data.EmailAddress, password);
        }
        catch(FormatException e)
        {
            await Forget();
            Console.WriteLine("Remember Me data was bad. Deleted it.");
        }
    }
    
    public async Task Remember(string emailAddress, string password)
    {
        await LocalStorage.SetItemAsync(LocalStorageKey, new RememberMeData
        {
            EmailAddress = emailAddress,
            Password = Security.Encrypt(password, EncryptionKey),
        });
    }

    public async Task Forget()
    {
        await LocalStorage.RemoveItemAsync(LocalStorageKey);
    }
    
    private sealed class RememberMeData
    {
        public required string EmailAddress { get; init; }
        public required string Password { get; init; }
    }
}