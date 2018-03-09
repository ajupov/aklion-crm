using System.Collections.Generic;
using Crm.Domain.Store;
using Crm.Domain.User;
using Crm.Domain.UserPermission;

namespace Crm.Domain.UserContext
{
    public class UserContextModel
    {
        public UserModel CurrentUser { get; set; }

        public StoreModel CurrentStore { get; set; }

        public List<UserPermissionModel> CurrentStorePermissions { get; set; }
    }
}