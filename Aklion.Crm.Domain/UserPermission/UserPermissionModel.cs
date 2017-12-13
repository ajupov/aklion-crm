using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserPermission
{
    public class UserPermissionModel : BaseModel
    {
        public int UserId { get; set; }

        public string UserLogin { get; }

        public int StoreId { get; set; }

        public string StoreName { get; }

        public Permission Permission { get; set; }
    }
}