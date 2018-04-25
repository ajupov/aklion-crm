namespace Crm.Models.User.OrderAttributeLink
{
    public class OrderAttributeLinkModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public string CreateDate { get; set; }
    }
}