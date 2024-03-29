﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderSource
{
    [Table("dbo.OrderSource as oso")]
    [Join("inner join dbo.Store as s on oso.StoreId = s.Id")]
    public class OrderSourceModel
    {
        [Column("oso.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("oso.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("oso.Name")]
        [AutocompleteOrSelect("oso.Name")]
        public string Name { get; set; }

        [Column("oso.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("oso.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}