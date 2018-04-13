using Infrastructure.Dao.Models;

namespace Crm.Models.User.ClientAttribute
{
    public class ClientAttributeParameterModel : BaseParameterModel
    {
        public string Key { get; set; }

        public string Name { get; set; }
    }
}