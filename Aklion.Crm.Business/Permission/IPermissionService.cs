using System.Collections.Generic;

namespace Aklion.Crm.Business.Permission
{
    public interface IPermissionService
    {
        Dictionary<string, Enums.Permission> GetForAdmin();

        Dictionary<string, Enums.Permission> GetForStoreOwner();
    }
}