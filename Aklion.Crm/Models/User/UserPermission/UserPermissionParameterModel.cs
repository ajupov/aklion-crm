using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.User.UserPermission
{
    public class UserPermissionParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public Permission? Permission { get; set; }

        public string CreateDate { get; set; }
    }
}