using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserToken
{
    public class UserTokenParameterModel
    {
        public int UserId { get; set; }

        public TokenType TokenType { get; set; }

        public string Token { get; set; }
    }
}