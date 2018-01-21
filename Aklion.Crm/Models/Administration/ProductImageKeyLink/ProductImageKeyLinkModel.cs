using System.IO;
using Microsoft.AspNetCore.Http;

namespace Aklion.Crm.Models.Administration.ProductImageKeyLink
{
    public class ProductImageKeyLinkModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int KeyId { get; set; }

        public string KeyName { get; set; }

        public string KeyDescription { get; set; }

        public IFormFile ImageFile { get; set; }

        public string Base64Value { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}