using System;
using Crm.Enums;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserToken
{
    [Table("dbo.UserToken as ut")]
    public class UserTokenModel
    {
        [Column("ut.Id")]
        [Identificator]
        public int Id { get; set; }

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
        public DateTime CreateDate { get; set; }

        [Column("ut.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}