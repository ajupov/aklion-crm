﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Order
{
    [Table("dbo.[Order] as o")]
    [Join("inner join dbo.Store as s on o.StoreId = s.Id " +
          "inner join dbo.Client as c on o.ClientId = c.Id " +
          "inner join dbo.OrderSource as oso on o.SourceId = oso.Id " +
          "inner join dbo.OrderStatus as ost on o.StatusId = ost.Id " +
          "inner join (select OrderId, sum(Price * Count) as [Sum] from dbo.OrderItem where IsDeleted = 0 group by OrderId) as i on o.Id = i.OrderId")]
    public class OrderModel
    {
        [Column("o.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("o.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("o.ClientId")]
        public int ClientId { get; set; }

        [Column("c.Name")]
        public string ClientName { get; }

        [Column("o.SourceId")]
        public int SourceId { get; set; }

        [Column("oso.Name")]
        public string SourceName { get; }

        [Column("o.StatusId")]
        public int StatusId { get; set; }

        [Column("ost.Name")]
        public string StatusName { get; }

        [Column("i.Sum")]
        public decimal TotalSum { get; set; }

        [Column("o.DiscountSum")]
        public decimal DiscountSum { get; set; }

        [Column("o.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("o.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("o.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}