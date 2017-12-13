using System;
using System.Linq;
using System.Security.Cryptography;

namespace Aklion.Infrastructure.Random
{
    public class RandomGenerator
    {
        public static string GenerateAlphaNumbericString(int length)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            var chars = new char[length];

            var random = new System.Random();

            for (var i = 0; i < length; i++)
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];

            return new string(chars);
        }

        public static string GenerateRandomInt(int size)
        {
            var random = new System.Random();
            var randomInt = random.Next(10 * size, 100 * size - 1);

            return randomInt.ToString(string.Concat(Enumerable.Repeat("0", size)));
        }

        public static string GenerateRandomCharacters(int size)
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