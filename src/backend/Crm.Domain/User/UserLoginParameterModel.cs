using Infrastructure.Dao.Attributes;

namespace Crm.Domain.User
{
    public class UserLoginParameterModel
    {
        [Where("u.Login = @Login")]
        public string Login { get; set; }
    }
}