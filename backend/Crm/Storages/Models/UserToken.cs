using System;
using Crm.Enums;

namespace Crm.Storages.Models
{
    public class UserToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public TokenType Type { get; set; }

        public string Value { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}