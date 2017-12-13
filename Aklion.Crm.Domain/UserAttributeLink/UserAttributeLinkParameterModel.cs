namespace Aklion.Crm.Domain.UserAttributeLink
{
    public class UserAttributeLinkParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }
    }
}