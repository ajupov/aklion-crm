using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.AuditLogger.Models;

namespace Infrastructure.AuditLogger
{
    public interface IAuditLogger
    {
        Task<List<AuditLogModel>> GetListAsync(AuditLogParameterModel model);

        void LogInserting(int userId, string userLogin, byte objectType, string objectTypeName, object newModel);

        void LogUpdating(int userId, string userLogin, byte objectType, string objectTypeName, object oldModel, object newModel);

        void LogDeleting(int userId, string userLogin, byte objectType, string objectTypeName, object oldModel);
    }
}