namespace Aklion.Crm.Models.User.UserAttributeLink
{
    public class UserAttributeLinkModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public string CreateDate { get; set; }
    }
}