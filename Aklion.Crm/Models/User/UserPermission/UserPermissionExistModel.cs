using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.User.UserPermission
{
    public class UserPermissionExistModel
    {
        public int UserId { get; set; }

        public Permission Permission { get; set; }

        public string PermissionName { get; set; }

        public bool IsExist { get; set; }
    }
}