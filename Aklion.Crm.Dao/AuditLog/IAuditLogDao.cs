using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.AuditLog;

namespace Aklion.Crm.Dao.AuditLog
{
    public interface IAuditLogDao
    {
        Task<Tuple<int, List<AuditLogModel>>> GetPagedListAsync(AuditLogParameterModel parameter);

        Task<AuditLogModel> GetAsync(int id);

        Task<int> CreateAsync(AuditLogModel model);
    }
}