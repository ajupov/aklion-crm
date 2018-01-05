using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderItem
{
    [Table("dbo.OrderItem as oi")]
    [Join("inner join dbo.Store as s on oi.StoreId = s.Id " +
          "inner join dbo.Order as o on oi.OrderId = o.Id " +
          "inner join dbo.Product as p on oi.ProductId = p.Id")]
    public class OrderItemModel : ICloneable
    {
        [Column("oi.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("oi.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("oi.OrderId")]
        public int OrderId { get; set; }

        [Column("oi.ProductId")]
        public int ProductId { get; set; }

        [Column("p.Name")]
        public string ProductName { get; }

        [Column("oi.Price")]
        public decimal Price { get; set; }

        [Column("oi.Count")]
        public int Count { get; set; }

        [Column("oi.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("oi.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("oi.ModifyDate")]
        public DateTime? ModifyDate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}