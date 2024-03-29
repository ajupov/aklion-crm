﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ClientAttribute
{
    [Table("dbo.ClientAttribute as ca")]
    [Join("inner join dbo.Store as s on ca.StoreId = s.Id")]
    public class ClientAttributeModel
    {
        [Column("ca.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("ca.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ca.[Key]")]
        public string Key { get; set; }

        [Column("ca.Name")]
        [AutocompleteOrSelect("ca.Name")]
        public string Name { get; set; }

        [Column("ca.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("ca.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("ca.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}