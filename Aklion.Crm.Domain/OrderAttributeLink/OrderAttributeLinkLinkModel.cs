namespace Aklion.Crm.Domain.OrderAttributeLink
{
    public class OrderAttributeLinkLinkModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int OrderId { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; }

        public string AttributeDescription { get; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }
    }
}