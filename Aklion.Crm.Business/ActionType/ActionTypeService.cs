using System;
using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.DisplayName;

namespace Aklion.Crm.Business.ActionType
{
    public class ActionTypeService : IActionTypeService
    {
        public Dictionary<string, AuditLogActionType> Get()
        {
            return GetAll().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        private static IEnumerable<AuditLogActionType> GetAll()
        {
            return Enum.GetValues(typeof(AuditLogActionType)).OfType<AuditLogActionType>().OrderBy(p => p);
        }
    }
}