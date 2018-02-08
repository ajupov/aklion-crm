using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.User.UserPermission
{
    public class UserPermissionParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public Permission? Permission { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}