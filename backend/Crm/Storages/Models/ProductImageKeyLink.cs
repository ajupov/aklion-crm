using System;

namespace Crm.Storages.Models
{
    public class ProductImageKeyLink
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int KeyId { get; set; }

        public ProductImageKey Key { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}