using Infrastructure.Dao.Attributes;

namespace Crm.Domain.User
{
    public class UserEmailParameterModel
    {
        [Where("u.Email = @Email")]
        public string Email { get; set; }
    }
}