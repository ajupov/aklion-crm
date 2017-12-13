using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserPermission
{
    public class UserPermissionParameterModel : BaseParameterModel
    {
        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public Permission? Permission { get; set; }
    }
}