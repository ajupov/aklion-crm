﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserAttributeLink
{
    [Table("dbo.UserAttributeLink as ual")]
    [Join("inner join dbo.Store as s on ual.StoreId = s.Id " +
          "inner join dbo.[User] as u on ual.UserId = u.Id " +
          "inner join dbo.UserAttribute as ua on ual.AttributeId = ua.Id")]
    public class UserAttributeLinkModel
    {
        [Column("ual.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("ual.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ual.UserId")]
        public int UserId { get; set; }

        [Column("u.Login")]
        public string UserLogin { get; }

        [Column("ual.AttributeId")]
        public int AttributeId { get; set; }

        [Column("ua.[Key]")]
        public string AttributeKey { get; }

        [Column("ua.Name")]
        public string AttributeName { get; }

        [Column("ual.Value")]
        public string Value { get; set; }

        [Column("ual.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("ual.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("ual.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}