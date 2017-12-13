using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.AuditLog;

namespace Aklion.Crm.Dao.AuditLog
{
    public interface IAuditLogDao
    {
        Task<BasePagingModel<AuditLogModel>> GetPagedList(AuditLogParameterModel parameterModel);

        Task<AuditLogModel> Get(int id);

        Task<int> Create(AuditLogModel model);
    }
}