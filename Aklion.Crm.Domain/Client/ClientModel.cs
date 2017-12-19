using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Client
{
    [Table("dbo.Client as c")]
    [Join("inner join dbo.Store as s on c.StoreId = s.Id")]
    public class ClientModel
    {
        [Column("c.Id")]
        [Identificator]
        public int Id { get; }

        [Column("c.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("c.Name")]
        public string Name { get; set; }

        [Column("c.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("c.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; }

        [Column("c.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; }
    }
}