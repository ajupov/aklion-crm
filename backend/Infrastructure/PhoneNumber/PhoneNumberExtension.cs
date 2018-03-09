using System.Linq;

namespace Infrastructure.PhoneNumber
{
    public static class PhoneNumberExtension
    {
        public static string ToFullPhoneNumber(this string shortPhoneNumber)
        {
            return $"+7{shortPhoneNumber}";
        }

        public static string ExtractPhoneNumber(this string phoneNumber)
        {
            var result = new string(phoneNumber.ToCharArray().Where(char.IsDigit).ToArray());

            const int russianPhoneNumberLength = 10;
            const char russianPhoneNumberFirstDigit = '7';

            if (result.Length > russianPhoneNumberLength && result[0] == russianPhoneNumberFirstDigit)
            {
                result = result.Substring(1, russianPhoneNumberLength);
            }

            return result;
        }
    }
}