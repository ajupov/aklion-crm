﻿using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserToken
{
    [Table("dbo.UserToken as ut")]
    public class UserTokenModel
    {
        [Column("ut.Id")]
        [Identificator]
        public int Id { get; }

        [Column("ut.UserId")]
        public int UserId { get; set; }

        [Column("ut.TokenType")]
        public TokenType TokenType { get; set; }

        [Column("ut.Token")]
        public string Token { get; set; }

        [Column("ut.ExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [Column("ut.IsUsed")]
        public bool IsUsed { get; set; }

        [Column("ut.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; }

        [Column("ut.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; }
    }
}