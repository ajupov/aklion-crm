﻿using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderAttribute
{
    [Table("dbo.OrderAttribute as oa")]
    [Join("inner join dbo.Store as s on oa.StoreId = s.Id")]
    public class OrderAttributeModel
    {
        [Column("oa.Id")]
        [Identificator]
        public int Id { get; }

        [Column("oa.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("oa.Name")]
        public string Name { get; set; }

        [Column("oa.Description")]
        public string Description { get; set; }

        [Column("oa.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("oa.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; }

        [Column("oa.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; }
    }
}