using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.AuditLog
{
    public class AuditLogModel : BaseModel
    {
        public int UserId { get; }

        public string UserLogin { get; }

        public int StoreId { get; set; }

        public string StoreName { get; }

        public AuditLogActionType ActionType { get; }

        public string OldValue { get; }

        public string NewValue { get; }

        public DateTime TimeStamp { get; }
    }
}