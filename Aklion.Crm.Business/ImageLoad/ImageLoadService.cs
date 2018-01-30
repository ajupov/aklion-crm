using System;
using System.IO;
using System.Threading.Tasks;
using Aklion.Infrastructure.FileFormat;
using Microsoft.AspNetCore.Http;

namespace Aklion.Crm.Business.ImageLoad
{
    public class ImageLoadService : IImageLoadService
    {
        public const string FsAvatarsPath = @"wwwroot\images\avatars\";
        public const string DbAvatarsPath = @"/images/avatars/";

        public Task<string> LoadAvatarImageAsync(IFormFile file)
        {
            return LoadAsync(file, FsAvatarsPath, DbAvatarsPath);
        }

        private static async Task<string> LoadAsync(IFormFile file, string fsPath, string dbPath)
        {
            if (!file.FileName.IsImage())
            {
                return string.Empty;
            }

            var guid = Guid.NewGuid().ToString();
            var extension = file.FileName.GetExtension();
            var path = $"{fsPath}{guid}.{extension}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream).ConfigureAwait(false);
            }

            return $"{dbPath}{guid}.{extension}";
        }
    }
}