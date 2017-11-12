﻿using System;

namespace Aklion.Infrastructure.Utils.RandomGenerator
{
    public class RandomGenerator
    {
        public static string GenerateAlphaNumbericString(int length)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            var chars = new char[length];

            var random = new Random();

            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}