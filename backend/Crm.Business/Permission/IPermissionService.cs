using System.Collections.Generic;

namespace Crm.Business.Permission
{
    public interface IPermissionService
    {
        List<Enums.Permission> GetForRegistration();

        List<Enums.Permission> GetForUser();

        Dictionary<string, Enums.Permission> GetForAdminWithNames();

        Dictionary<string, Enums.Permission> GetForUserWithNames();

        Dictionary<string, Enums.Permission> GetWithNames(IEnumerable<Enums.Permission> permissions);

        IEnumerable<Enums.Permission> GetAll();
    }
}