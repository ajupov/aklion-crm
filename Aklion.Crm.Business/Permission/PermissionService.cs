using System;
using System.Collections.Generic;
using System.Linq;
using Aklion.Infrastructure.DisplayName;

namespace Aklion.Crm.Business.Permission
{
    public class PermissionService : IPermissionService
    {
        public Dictionary<string, Enums.Permission> GetForAdmin()
        {
            return GetAll().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public Dictionary<string, Enums.Permission> GetForStoreOwner()
        {
            return GetAll().Where(p => p != Enums.Permission.Admin).ToDictionary(k => k.GetDisplayName(), v => v);
        }

        private static IEnumerable<Enums.Permission> GetAll()
        {
            return Enum.GetValues(typeof(Enums.Permission)).OfType<Enums.Permission>().OrderBy(p => p);
        }
    }
}