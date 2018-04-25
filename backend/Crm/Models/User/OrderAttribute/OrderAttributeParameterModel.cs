using Infrastructure.Dao.Models;

namespace Crm.Models.User.OrderAttribute
{
    public class OrderAttributeParameterModel : BaseParameterModel
    {
        public string Name { get; set; }

        public string Key { get; set; }
    }
}