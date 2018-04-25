using Infrastructure.Dao.Models;

namespace Crm.Models.User.UserAttribute
{
    public class UserAttributeParameterModel : BaseParameterModel
    {
        public string Key { get; set; }

        public string Name { get; set; }
    }
}