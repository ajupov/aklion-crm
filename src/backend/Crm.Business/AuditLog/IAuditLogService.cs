using System.Collections.Generic;
using Crm.Enums;
using Infrastructure.AuditLogger.Enums;

namespace Crm.Business.AuditLog
{
    public interface IAuditLogService
    {
        Dictionary<string, AuditLogActionType> GetActionTypes();

        Dictionary<string, AuditLogObjectType> GetObjectTypes();
    }
}