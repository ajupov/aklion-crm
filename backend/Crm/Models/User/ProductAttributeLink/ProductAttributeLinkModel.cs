namespace Crm.Models.User.ProductAttributeLink
{
    public class ProductAttributeLinkModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public string CreateDate { get; set; }
    }
}