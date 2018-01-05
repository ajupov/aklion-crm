using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Administration.AuditLog
{
    public class AuditLogParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public AuditLogActionType? ActionType { get; set; }

        public AuditLogObjectType? ObjectType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string TimeStamp { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}