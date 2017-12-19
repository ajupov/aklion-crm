using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.User
{
    public class UserEmailParameterModel
    {
        [Where("u.Email = @Email")]
        public string Email { get; set; }
    }
}