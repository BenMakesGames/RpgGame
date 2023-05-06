using Blazored.LocalStorage;
using RpgGame.Functions;

namespace RpgGame.Services;

public sealed class RememberMe
{
    private const string LocalStorageKey = "remember-me";
    private const string ConfigurationSectionName = "RememberMe";
    
    private ILocalStorageService LocalStorage { get; }
    private CurrentPlayer CurrentPlayer { get; }
    
    private RememberMeConfig Config { get; }

    public RememberMe(
        ILocalStorageService localStorage, CurrentPlayer currentPlayer, IConfiguration config
    )
    {
        LocalStorage = localStorage;
        CurrentPlayer = currentPlayer;
        Config = config.GetSection(ConfigurationSectionName).Get<RememberMeConfig>()
            ?? throw new Exception($"{ConfigurationSectionName} section is missing from appsettings.");
    }

    public async Task LogIn()
    {
        var data = await LocalStorage.GetItemAsync<RememberMeData>(LocalStorageKey);
        
        if (data is null)
            return;

        string? password;
        
        try
        {
            password = Security.Decrypt(data.Password, Config.EncryptionKey);
        }
        catch(Exception)
        {
            await Forget();
            return;
        }
        
        var result = await CurrentPlayer.LogInAsync(data.EmailAddress, password);

        if(!result.Success)
            await Forget();
    }
    
    public async Task Remember(string emailAddress, string password)
    {
        await LocalStorage.SetItemAsync(LocalStorageKey, new RememberMeData
        {
            EmailAddress = emailAddress,
            Password = Security.Encrypt(password, Config.EncryptionKey),
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

public sealed class RememberMeConfig
{
    public required string EncryptionKey { get; init; }
}