using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.User
{
    public class UserLoginParameterModel
    {
        [Where("u.Login = @Login")]
        public string Login { get; set; }
    }
}