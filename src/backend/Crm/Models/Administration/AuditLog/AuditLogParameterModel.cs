using Crm.Enums;
using Infrastructure.AuditLogger.Enums;

namespace Crm.Models.Administration.AuditLog
{
    public class AuditLogParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public AuditLogActionType? ActionType { get; set; }

        public string ActionTypeName { get; set; }

        public AuditLogObjectType? ObjectType { get; set; }

        public string ObjectTypeName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string TimeStamp { get; set; }

        public bool IsAscendingSortingOrder { get; set; }

        public int? Offset { get; set; }

        public int? Size { get; set; }
    }
}