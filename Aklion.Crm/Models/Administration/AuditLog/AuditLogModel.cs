using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Administration.AuditLog
{
    public class AuditLogModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public AuditLogActionType ActionType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string TimeStamp { get; set; }
    }
}