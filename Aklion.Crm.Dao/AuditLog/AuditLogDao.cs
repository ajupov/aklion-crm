using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.AuditLog;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.AuditLog
{
    public class AuditLogDao : IAuditLogDao
    {
        private readonly IDao _dao;

        public AuditLogDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<AuditLogModel>>> GetPagedListAsync(AuditLogParameterModel parameter)
        {
            return _dao.GetPagedListAsync<AuditLogModel, AuditLogParameterModel>(parameter);
        }

        public Task<AuditLogModel> GetAsync(int id)
        {
            return _dao.GetAsync<AuditLogModel>(id);
        }

        public Task<int> CreateAsync(AuditLogModel model)
        {
            return _dao.CreateAsync(model);
        }
    }
}