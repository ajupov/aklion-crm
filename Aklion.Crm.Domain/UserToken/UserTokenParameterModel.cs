using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserToken
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