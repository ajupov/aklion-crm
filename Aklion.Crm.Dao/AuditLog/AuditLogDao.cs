using System.Threading.Tasks;
using Aklion.Crm.Dao.AuditLog.Resources;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.AuditLog;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.AuditLog
{
    public class AuditLogDao : IAuditLogDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public AuditLogDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<BasePagingModel<AuditLogModel>> GetPagedList(AuditLogParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectMultipleAsync(Queries.GetPagedList, async r => new BasePagingModel<AuditLogModel>
            {
                TotalCount = await r.SelectOneAsync<int>().ConfigureAwait(false),
                List = await r.SelectListAsync<AuditLogModel>().ConfigureAwait(false),
            }, parameterModel);
        }

        public Task<AuditLogModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<AuditLogModel>(Queries.Get, new {id});
        }

        public Task<int> Create(AuditLogModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }
    }
}