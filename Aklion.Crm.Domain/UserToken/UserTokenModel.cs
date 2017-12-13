using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserToken
{
    public class UserTokenModel : BaseModel
    {
        public int UserId { get; set; }

        public TokenType TokenType { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsUsed { get; set; }
    }
}