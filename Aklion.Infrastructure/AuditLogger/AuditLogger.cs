using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Infrastructure.AuditLogger.Enums;
using Aklion.Infrastructure.AuditLogger.Models;
using Aklion.Infrastructure.DisplayName;
using Aklion.Infrastructure.Json;
using Aklion.Infrastructure.Logger;

namespace Aklion.Infrastructure.AuditLogger
{
    public class AuditLogger : IAuditLogger
    {
        private static string Tag;
        private readonly ILogger _logger;

        public AuditLogger(ILogger logger)
        {
            Tag = GetType().Name;
            _logger = logger;
        }

        public Task<(int TotalCount, List<AuditLogModel> List)> GetPagedListAsync(AuditLogParameterModel model)
        {
            throw new NotImplementedException();
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
                TimeStamp = System.DateTime.Now
            };

            _logger.Info(Tag, null, model);
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
                TimeStamp = System.DateTime.Now
            };

            _logger.Info(Tag, null, model);
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
                TimeStamp = System.DateTime.Now
            };

            _logger.Info(Tag, null, model);
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
                return AuditLogObjectType.None;

            name = name.Replace("Model", string.Empty);

            if (Enum.TryParse(typeof(AuditLogObjectType), name, out var parseResult))
                return (AuditLogObjectType) parseResult;
            ;

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