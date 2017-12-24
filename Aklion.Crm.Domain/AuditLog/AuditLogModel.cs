using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.AuditLog
{
    [Table("dbo.AuditLog as al")]
    [Join("inner join dbo.[User] as u on al.UserId = u.Id " +
          "inner join dbo.Store as s on al.StoreId = s.Id")]
    public class AuditLogModel
    {
        [Column("al.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("al.UserId")]
        public int UserId { get; set; }

        [Column("u.Login")]
        public string UserLogin { get; }

        [Column("al.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("al.ActionType")]
        public AuditLogActionType ActionType { get; set; }

        [Column("al.OldValue")]
        public string OldValue { get; set; }

        [Column("al.NewValue")]
        public string NewValue { get; set; }

        [Column("al.TimeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}