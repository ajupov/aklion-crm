using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.AuditLog
{
    public class AuditLogParameterModel : BaseParameterModel
    {
        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public AuditLogActionType? ActionType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}