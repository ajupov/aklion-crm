namespace Infrastructure.AuditLogger.Models
{
    public class AuditLogModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public byte ActionType { get; set; }

        public string ActionTypeName { get; set; }

        public byte ObjectType { get; set; }

        public string ObjectTypeName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string TimeStamp { get; set; }
    }
}