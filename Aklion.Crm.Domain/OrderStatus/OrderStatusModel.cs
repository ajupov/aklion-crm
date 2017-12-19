using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderStatus
{
    [Table("dbo.OrderStatus as ost")]
    [Join("inner join dbo.Store as s on ost.StoreId = s.Id")]
    public class OrderStatusModel
    {
        [Column("ost.Id")]
        [Identificator]
        public int Id { get; }

        [Column("ost.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ost.Name")]
        public string Name { get; set; }

        [Column("ost.CreateDate")]
        public DateTime CreateDate { get; }

        [Column("ost.ModifyDate")]
        public DateTime? ModifyDate { get; }
    }
}