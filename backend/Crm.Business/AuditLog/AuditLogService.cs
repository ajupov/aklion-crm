using System;
using System.Collections.Generic;
using System.Linq;
using Crm.Enums;
using Infrastructure.AuditLogger.Enums;
using Infrastructure.DisplayName;

namespace Crm.Business.AuditLog
{
    public class AuditLogService : IAuditLogService
    {
        public Dictionary<string, AuditLogActionType> GetActionTypes()
        {
            return GetAllActionTypes().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public Dictionary<string, AuditLogObjectType> GetObjectTypes()
        {
            return GetAllObjectTypes().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        private static IEnumerable<AuditLogActionType> GetAllActionTypes()
        {
            return Enum.GetValues(typeof(AuditLogActionType)).OfType<AuditLogActionType>().OrderBy(p => p);
        }

        private static IEnumerable<AuditLogObjectType> GetAllObjectTypes()
        {
            return Enum.GetValues(typeof(AuditLogObjectType)).OfType<AuditLogObjectType>().OrderBy(p => p);
        }
    }
}