using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductStatus
{
    [Table("dbo.ProductStatus as ps")]
    [Join("inner join dbo.Store as s on ps.StoreId = s.Id")]
    public class ProductStatusModel
    {
        [Column("ps.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("ps.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ps.Name")]
        public string Name { get; set; }

        [Column("ps.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("ps.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}