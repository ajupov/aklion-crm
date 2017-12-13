namespace Aklion.Crm.Domain.UserAttributeLink
{
    public class UserAttributeLinkModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int UserId { get; set; }

        public string UserLogin { get; }

        public int AttributeId { get; set; }

        public string AttributeName { get; }

        public string AttributeDescription { get; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }
    }
}