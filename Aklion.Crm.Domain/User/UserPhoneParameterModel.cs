using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.User
{
    public class UserPhoneParameterModel
    {
        [Where("u.Phone = @Phone")]
        public string Phone { get; set; }
    }
}