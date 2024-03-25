using System.Security.Cryptography;

namespace Olx.Service;

public class PasswordHasher
{
    [Obsolete("Obsolete")]
    public static (string Hash, byte[] Salt) GenerateHash(string password)
    {
        byte[] salt = CreateSalt(16); // 16 bytes = 128 bits

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); // 32 bytes = 256 bits

        return (Convert.ToBase64String(hash), salt);
    }
    
    [Obsolete("Obsolete")]
    private static byte[] CreateSalt(int size)
    {
        using var rng = new RNGCryptoServiceProvider();
        byte[] salt = new byte[size];
        rng.GetBytes(salt);
        return salt;
    }
}
