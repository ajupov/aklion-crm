﻿namespace Crm.Models.Administration.ProductAttribute
{
    public class ProductAttributeModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}