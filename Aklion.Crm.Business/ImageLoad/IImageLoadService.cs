using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aklion.Crm.Business.ImageLoad
{
    public interface IImageLoadService
    {
        Task<string> LoadAvatarImageAsync(IFormFile file);
    }
}