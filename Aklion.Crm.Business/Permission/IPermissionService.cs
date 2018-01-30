using System.Collections.Generic;

namespace Aklion.Crm.Business.Permission
{
    public interface IPermissionService
    {
        List<Enums.Permission> GetForUser();

        Dictionary<string, Enums.Permission> GetForAdminWithNames();

        Dictionary<string, Enums.Permission> GetForUserWithNames();
    }
}