namespace PetGame.Services;

public static class Security
{
    /// <summary>
    /// PasswordHashWorkFactor determines how much work is done to hash a player's password.
    ///
    /// 11 is the current (2023) recommended value; in a few years, you might want to tick this value up, to account
    /// for the increasing power of computers. This is something you should occasionally Google about, to see the
    /// latest recommendations.
    ///
    /// Don't just increase PasswordHashWorkFactor up to some huge number! Doing so could create a lot of work for your
    /// server, causing logins to take a long time, or even allow a malicious user to crash your server by asking it to
    /// do a lot of logins at once!
    /// </summary>
    public const int PasswordHashWorkFactor = 11;

    public static string HashPassword(string plainTextPassword)
        => BCrypt.Net.BCrypt.HashPassword(plainTextPassword, PasswordHashWorkFactor, enhancedEntropy: true);

    public static bool PasswordNeedsRehash(string hashedPassword)
        => BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, PasswordHashWorkFactor);

    public static bool PasswordsMatch(string plainTextPassword, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword, enhancedEntropy: true);
}