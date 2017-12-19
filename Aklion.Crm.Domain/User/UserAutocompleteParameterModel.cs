using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.User
{
    [WhereCombination("and")]
    public class UserAutocompleteParameterModel
    {
        [Where("@Login is null or u.Login like @Login + '%'")]
        public string Login { get; set; }
    }
}