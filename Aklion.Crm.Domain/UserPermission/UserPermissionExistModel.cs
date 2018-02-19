using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserPermission
{
    public class UserPermissionExistModel
    {
        public Permission Permission { get; set; }

        public bool IsExist { get; set; }
    }
}