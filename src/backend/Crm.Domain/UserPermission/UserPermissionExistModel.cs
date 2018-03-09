using Crm.Enums;

namespace Crm.Domain.UserPermission
{
    public class UserPermissionExistModel
    {
        public Permission Permission { get; set; }

        public bool IsExist { get; set; }
    }
}