using System.Collections.Generic;
using Aklion.Crm.Domain.Store;
using Aklion.Crm.Domain.User;
using Aklion.Crm.Domain.UserPermission;

namespace Aklion.Crm.Domain.UserContext
{
    public class UserContextModel
    {
        public UserModel CurrentUser { get; set; }

        public StoreModel CurrentStore { get; set; }

        public List<UserPermissionModel> CurrentStorePermissions { get; set; }
    }
}