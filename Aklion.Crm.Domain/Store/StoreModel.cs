using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Store
{
    [Table("dbo.Store as s")]
    public class StoreModel
    {
        [Column("ps.Id")]
        [Identificator]
        public int Id { get; }

        [Column("s.Name")]
        public string Name { get; set; }

        [Column("s.ApiSecret")]
        public string ApiSecret { get; set; }

        [Column("s.IsLocked")]
        public bool IsLocked { get; set; }

        [Column("s.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("s.CreateDate")]
        public DateTime CreateDate { get; }

        [Column("s.ModifyDate")]
        public DateTime? ModifyDate { get; }
    }
}