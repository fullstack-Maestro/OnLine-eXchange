using System.Security.Cryptography;

namespace Olx.Service;

public class PasswordChecker
{
    public static bool VerifyPassword(string password, string storedHash, byte[] storedSalt)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = storedSalt;

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] newHash = pbkdf2.GetBytes(32);

        return hashBytes.Equals(newHash);
    }
}
