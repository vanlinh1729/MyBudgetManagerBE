using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Infrastructure.Services.Auth;

public class PasswordHasher : IPasswordHasher
{
    private const int Version = 1;
    private const int SaltSize = 16;    // 128-bit
    private const int KeySize = 32;     // 256-bit
    private const int Iterations = 310000;

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations,
            numBytesRequested: KeySize
        );

        return $"{Version}.{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string saved, string password)
    {
        var parts = saved.Split('.');
        if (parts.Length != 4)
            return false;

        int version = int.Parse(parts[0]);
        int iterations = int.Parse(parts[1]);
        byte[] salt = Convert.FromBase64String(parts[2]);
        byte[] storedHash = Convert.FromBase64String(parts[3]);

        byte[] computedHash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: iterations,
            numBytesRequested: storedHash.Length
        );

        return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
    }
}