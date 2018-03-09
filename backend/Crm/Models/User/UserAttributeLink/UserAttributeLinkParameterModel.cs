namespace Crm.Models.User.UserAttributeLink
{
    public class UserAttributeLinkParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeKey { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}