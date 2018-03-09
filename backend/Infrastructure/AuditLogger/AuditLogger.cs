using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.AuditLogger.Enums;
using Infrastructure.AuditLogger.Models;
using Infrastructure.Json;
using Infrastructure.Logger;

namespace Infrastructure.AuditLogger
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

        public Task<List<AuditLogModel>> GetListAsync(AuditLogParameterModel model)
        {
            throw new NotImplementedException();
        }

        public void LogInserting(int userId, string userLogin, byte objectType, string objectTypeName, object newModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                UserLogin = userLogin,
                ActionType = (byte) AuditLogActionType.Insert,
                ActionTypeName = "Вставка",
                ObjectType = objectType,
                ObjectTypeName = objectTypeName,
                OldValue = null,
                NewValue = newModel.ToJsonString(),
                TimeStamp = System.DateTime.Now.ToString("O")
            };

            _logger.Info(Tag, null, model);
        }

        public void LogUpdating(int userId, string userLogin, byte objectType, string objectTypeName, object oldModel, object newModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                UserLogin = userLogin,
                ActionType = (byte) AuditLogActionType.Update,
                ActionTypeName = "Обновление",
                ObjectType = objectType,
                ObjectTypeName = objectTypeName,
                OldValue = oldModel.ToJsonString(),
                NewValue = newModel.ToJsonString(),
                TimeStamp = System.DateTime.Now.ToString("O")
            };

            _logger.Info(Tag, null, model);
        }

        public void LogDeleting(int userId, string userLogin, byte objectType, string objectTypeName, object oldModel)
        {
            var model = new AuditLogModel
            {
                UserId = userId,
                UserLogin = userLogin,
                ActionType = (byte) AuditLogActionType.Delete,
                ActionTypeName = "Удаление",
                ObjectType = objectType,
                ObjectTypeName = objectTypeName,
                OldValue = oldModel.ToJsonString(),
                NewValue = null,
                TimeStamp = System.DateTime.Now.ToString("O")
            };

            _logger.Info(Tag, null, model);
        }
    }
}