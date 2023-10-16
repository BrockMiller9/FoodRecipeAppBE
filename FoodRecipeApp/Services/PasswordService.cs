using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace FoodRecipeApp.Services
{
    public class PasswordService
    {
        public (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHashAndSalt(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = HashPassword(password, salt);
            return (Encoding.ASCII.GetBytes(hashedPassword), salt);
        }
        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public bool ValidatePassword(string password, byte[] salt, byte[] existingHash)
        {
            string hashed = HashPassword(password, salt);
            return hashed == Encoding.ASCII.GetString(existingHash);
        }
    }
}
