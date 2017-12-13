namespace Aklion.Crm.Domain.ClientAttributeLink
{
    public class ClientAttributeLinkParameterModel :BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ClientId { get; set; }

        public string ClientName { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }
    }
}