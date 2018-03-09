using Crm.Enums;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserToken
{
    [WhereCombination("and")]
    public class UserTokenParameterModel
    {
        [Where("ut.UserId = @UserId")]
        public int UserId { get; set; }

        [Where("ut.TokenType = @TokenType")]
        public TokenType TokenType { get; set; }

        [Where("ut.Token = @Token")]
        public string Token { get; set; }
    }
}