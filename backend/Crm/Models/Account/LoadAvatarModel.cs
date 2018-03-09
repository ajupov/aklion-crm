using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Crm.Models.Account
{
    public class LoadAvatarModel
    {
        [Required]
        public IFormFile AvatarFile { get; set; }
    }
}