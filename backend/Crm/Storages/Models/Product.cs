using System;
using System.Collections.Generic;

namespace Crm.Storages.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int? ParentProductId { get; set; }

        public Product ParentProduct { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public ProductStatus Status { get; set; }

        public string VendorCode { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public List<ProductAttributeLink> AttributeLinks { get; set; }
    }
}