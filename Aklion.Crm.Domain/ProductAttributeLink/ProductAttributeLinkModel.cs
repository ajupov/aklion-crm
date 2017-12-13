namespace Aklion.Crm.Domain.ProductAttributeLink
{
    public class ProductAttributeLinkModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int ProductId { get; set; }

        public string ProductName { get; }

        public int AttributeId { get; set; }

        public string AttributeName { get; }

        public string AttributeDescription { get; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }
    }
}