using System.Linq;

namespace Aklion.Infrastructure.Utils.File
{
    public static class FileFormatExtension
    {
        private static readonly string[] Extensions = {"jpg", "jpeg", "png", "bmp"};

        public static bool IsImage(this string fileName)
        {
            return Extensions.Contains(GetExtension(fileName));
        }

        public static string GetExtension(this string fileName)
        {
            return fileName.Split('.').LastOrDefault();
        }
    }
}