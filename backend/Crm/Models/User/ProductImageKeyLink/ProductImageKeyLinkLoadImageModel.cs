using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Crm.Models.User.ProductImageKeyLink
{
    public class ProductImageKeyLinkLoadImageModel
    {
        public int Id { get; set; }
        
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}