using Aklion.Infrastructure.AuditLogger.Enums;

namespace Aklion.Infrastructure.AuditLogger.Models
{
    public class AuditLogModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public AuditLogActionType ActionType { get; set; }

        public AuditLogObjectType ObjectType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public System.DateTime TimeStamp { get; set; }
    }
}