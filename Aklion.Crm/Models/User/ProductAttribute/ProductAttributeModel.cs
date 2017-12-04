using System;

namespace Aklion.Crm.Models.User.ProductAttribute
{
    public class ProductAttributeModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }
    }
}