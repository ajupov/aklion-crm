﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ProductImageKeyLink
{
    [Table("dbo.ProductImageKeyLink as pikl")]
    [Join("inner join dbo.Store as s on pikl.StoreId = s.Id " +
          "inner join dbo.Product as p on pikl.ProductId = p.Id " +
          "inner join dbo.ProductImageKey as pik on pikl.KeyId = pik.Id")]
    public class ProductImageKeyLinkModel
    {
        [Column("pikl.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("pikl.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("pikl.ProductId")]
        public int ProductId { get; set; }

        [Column("p.Name")]
        public string ProductName { get; }

        [Column("pikl.KeyId")]
        public int KeyId { get; set; }

        [Column("pik.[Key]")]
        public string KeyKey { get; }

        [Column("pik.Name")]
        public string KeyName { get; }

        [Column("pikl.Value")]
        public byte[] Value { get; set; }

        [Column("pikl.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("pikl.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("pikl.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}