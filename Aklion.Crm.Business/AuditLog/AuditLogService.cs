using System;
using Aklion.Crm.Dao.AuditLog;
using Aklion.Crm.Domain.AuditLog;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Json;

namespace Aklion.Crm.Business.AuditLog
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogDao _auditLogDao;

        public AuditLogService(IAuditLogDao auditLogDao)
        {
            _auditLogDao = auditLogDao;
        }

        public void LogInserting(int userId, int storeId, object newModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                StoreId = storeId,
                ActionType = AuditLogActionType.Insert,
                OldValue = null,
                NewValue = newModel.ToJsonString(),
                TimeStamp = DateTime.Now
            };

            _auditLogDao.CreateAsync(model);
        }

        public void LogUpdating(int userId, int storeId, object oldModel, object newModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                StoreId = storeId,
                ActionType = AuditLogActionType.Update,
                OldValue = oldModel.ToJsonString(),
                NewValue = newModel.ToJsonString(),
                TimeStamp = DateTime.Now
            };

            _auditLogDao.CreateAsync(model);
        }

        public void LogDeleting(int userId, int storeId, object oldModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                StoreId = storeId,
                ActionType = AuditLogActionType.Delete,
                OldValue = oldModel.ToJsonString(),
                NewValue = null,
                TimeStamp = DateTime.Now
            };

            _auditLogDao.CreateAsync(model);
        }
    }
}