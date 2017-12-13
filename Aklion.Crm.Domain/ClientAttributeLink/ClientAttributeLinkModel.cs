namespace Aklion.Crm.Domain.ClientAttributeLink
{
    public class ClientAttributeLinkModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int ClientId { get; set; }

        public string ClientName { get; }

        public int AttributeId { get; set; }

        public string AttributeName { get; }

        public string AttributeDescription { get; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }
    }
}