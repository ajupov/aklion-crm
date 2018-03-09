using Infrastructure.Dao.Attributes;

namespace Crm.Domain.User
{
    public class UserPhoneParameterModel
    {
        [Where("u.Phone = @Phone")]
        public string Phone { get; set; }
    }
}