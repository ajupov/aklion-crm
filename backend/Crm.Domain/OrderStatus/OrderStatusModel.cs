﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderStatus
{
    [Table("dbo.OrderStatus as ost")]
    [Join("inner join dbo.Store as s on ost.StoreId = s.Id")]
    public class OrderStatusModel
    {
        [Column("ost.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("ost.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ost.Name")]
        [AutocompleteOrSelect("ost.Name")]
        public string Name { get; set; }

        [Column("ost.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("ost.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}