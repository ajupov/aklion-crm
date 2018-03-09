using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DisplayName;

namespace Crm.Business.Permission
{
    public class PermissionService : IPermissionService
    {
        public List<Enums.Permission> GetForRegistration()
        {
            return GetAll().Where(p => p != Enums.Permission.Admin).ToList();
        }

        public List<Enums.Permission> GetForUser()
        {
            return GetAll().Where(p => p != Enums.Permission.Admin && p != Enums.Permission.None).ToList();
        }

        public Dictionary<string, Enums.Permission> GetForAdminWithNames()
        {
            return GetAll().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public Dictionary<string, Enums.Permission> GetForUserWithNames()
        {
            return GetAll().Where(p => p != Enums.Permission.Admin).ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public Dictionary<string, Enums.Permission> GetWithNames(IEnumerable<Enums.Permission> permisssions)
        {
            return permisssions.ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public IEnumerable<Enums.Permission> GetAll()
        {
            return Enum.GetValues(typeof(Enums.Permission)).OfType<Enums.Permission>().OrderBy(p => p);
        }
    }
}