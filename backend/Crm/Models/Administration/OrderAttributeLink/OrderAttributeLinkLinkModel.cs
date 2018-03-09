namespace Crm.Models.Administration.OrderAttributeLink
{
    public class OrderAttributeLinkModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int OrderId { get; set; }

        public int AttributeId { get; set; }

        public string AttributeKey { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}