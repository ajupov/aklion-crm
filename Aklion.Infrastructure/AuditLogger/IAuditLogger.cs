using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.AuditLogger.Enums;
using Aklion.Infrastructure.AuditLogger.Models;

namespace Aklion.Infrastructure.AuditLogger
{
    public interface IAuditLogger
    {
        Task<(int TotalCount, List<AuditLogModel> List)> GetPagedListAsync(AuditLogParameterModel model);

        void LogInserting(int userId, int storeId, object newModel);

        void LogUpdating(int userId, int storeId, object oldModel, object newModel);

        void LogDeleting(int userId, int storeId, object oldModel);

        Dictionary<string, AuditLogActionType> GetActionTypes();

        Dictionary<string, AuditLogObjectType> GetObjectTypes();
    }
}