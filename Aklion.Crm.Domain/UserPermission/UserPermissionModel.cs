using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserPermission
{
    [Table("dbo.UserPermission as up")]
    [Join("inner join dbo.[User] as u on up.UserId = u.Id " +
          "inner join dbo.Store as s on up.StoreId = s.Id")]
    public class UserPermissionModel
    {
        [Column("up.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("up.UserId")]
        public int UserId { get; set; }

        [Column("u.Login")]
        public string UserLogin { get; }

        [Column("up.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("up.Permission")]
        public Permission Permission { get; set; }

        [Column("up.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("up.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}