using System.Security.Cryptography;
using System.Text;

namespace PetGame.Functions;

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
    
    /// <summary>
    /// Encrypts the given value using AES-256, and returns the result as a Base64-encoded string.
    /// </summary>
    public static string Encrypt(string value, string key)
    {
        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.GenerateIV();

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();

        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(value);
            }
        }

        var iv = aes.IV;
        var encryptedBytes = ms.ToArray();

        var result = new byte[iv.Length + encryptedBytes.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Decrypts the given Base64-encoded value using AES-256, which must have been encrypted using the same key. 
    /// </summary>
    public static string Decrypt(string value, string key)
    {
        var valueBytes = Convert.FromBase64String(value);

        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(key);

        var iv = new byte[aes.BlockSize / 8];
        var encryptedBytes = new byte[valueBytes.Length - iv.Length];

        Buffer.BlockCopy(valueBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(valueBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

        var decryptor = aes.CreateDecryptor(aes.Key, iv);

        using var ms = new MemoryStream(encryptedBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}