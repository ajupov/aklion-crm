﻿namespace Aklion.Crm.Models.User.ProductCategory
{
    public class ProductCategoryParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CreateDate { get; set; }
    }
}