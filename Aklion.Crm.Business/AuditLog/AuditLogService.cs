using System;
using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Dao.AuditLog;
using Aklion.Crm.Domain.AuditLog;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.DisplayName;
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
                ObjectType = GetObjectType(newModel),
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
                ObjectType = GetObjectType(newModel),
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
                ObjectType = GetObjectType(oldModel),
                OldValue = oldModel.ToJsonString(),
                NewValue = null,
                TimeStamp = DateTime.Now
            };

            _auditLogDao.CreateAsync(model);
        }

        public Dictionary<string, AuditLogActionType> GetActionTypes()
        {
            return GetAllActionTypes().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        public Dictionary<string, AuditLogObjectType> GetObjectTypes()
        {
            return GetAllObjectTypes().ToDictionary(k => k.GetDisplayName(), v => v);
        }

        private static AuditLogObjectType GetObjectType(object model)
        {
            var name = model.GetType().Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                return AuditLogObjectType.None;
            }

            name = name.Replace("Model", string.Empty);

            if (Enum.TryParse(typeof(AuditLogObjectType), name, out var parseResult))
            {
                return (AuditLogObjectType) parseResult;
            };

            return AuditLogObjectType.None;
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