using System.Collections.Generic;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Business.AuditLog
{
    public interface IAuditLogService
    {
        void LogInserting(int userId, int storeId, object newModel);

        void LogUpdating(int userId, int storeId, object oldModel, object newModel);

        void LogDeleting(int userId, int storeId, object oldModel);

        Dictionary<string, AuditLogActionType> GetActionTypes();

        Dictionary<string, AuditLogObjectType> GetObjectTypes();
    }
}