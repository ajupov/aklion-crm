using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Crm.Models.Administration.ProductImageKeyLink
{
    public class ProductImageKeyLinkLoadImageModel
    {
        public int Id { get; set; }
        
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}