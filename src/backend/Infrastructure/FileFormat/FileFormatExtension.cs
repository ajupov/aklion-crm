using System.Linq;

namespace Infrastructure.FileFormat
{
    public static class FileFormatExtension
    {
        private static readonly string[] ImageExtensions = {"jpg", "jpeg", "png", "bmp"};

        public static bool IsImage(this string fileName)
        {
            return ImageExtensions.Contains(GetExtension(fileName));
        }

        public static string GetExtension(this string fileName)
        {
            return fileName.Split('.').LastOrDefault();
        }
    }
}