using System;
using System.Linq;
using System.Security.Cryptography;
using Aklion.Crm.Enums;

namespace Aklion.Infrastructure.Utils.Token
{
    public static class TokenHelper
    {
        public static string GenerateToken(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.EmailConfirmation:
                    return GenerateRandomCharacters(127);
                case TokenType.PhoneConfirmation:
                    return GenerateRandomInt(4);
                case TokenType.PasswordReset:
                    return GenerateRandomCharacters(127);
                default:
                    return null;
            }
        }

        public static System.DateTime GetExpirationDate(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.EmailConfirmation:
                    return System.DateTime.Now.AddDays(1);
                case TokenType.PhoneConfirmation:
                    return System.DateTime.Now.AddMinutes(10);
                case TokenType.PasswordReset:
                    return System.DateTime.Now.AddDays(1);
                default:
                    return System.DateTime.Now;
            }
        }

        private static string GenerateRandomInt(int size)
        {
            var random = new Random();
            var randomInt = random.Next(10 * size, 100 * size - 1);

            return randomInt.ToString(string.Concat(Enumerable.Repeat("0", size)));
        }

        private static string GenerateRandomCharacters(int size)
        {
            var bytes = new byte[size];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}