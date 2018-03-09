using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Store
{
    [Table("dbo.Store as s")]
    [Join("inner join dbo.UserPermission as up on s.Id = up.StoreId")]
    public class StoreByUserModel
    {
        [Column("s.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("up.UserId")]
        public int UserId { get; set; }

        [Column("s.Name")]
        [AutocompleteOrSelect("s.Name")]
        public string Name { get; set; }

        [Column("s.ApiSecret")]
        public string ApiSecret { get; set; }

        [Column("s.IsLocked")]
        public bool IsLocked { get; set; }

        [Column("s.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("s.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("s.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}