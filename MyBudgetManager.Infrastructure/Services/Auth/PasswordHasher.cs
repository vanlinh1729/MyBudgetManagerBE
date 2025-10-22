using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Infrastructure.Services.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Tạo salt 128-bit
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        // Hash bằng PBKDF2
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Lưu salt + hash
        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    public bool VerifyPassword(string hash, string password)
    {
        var parts = hash.Split('.');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return storedHash == computedHash;
    }
}