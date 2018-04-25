using Infrastructure.Dao.Models;

namespace Crm.Models.User.OrderAttributeLink
{
    public class OrderAttributeLinkParameterModel : BaseParameterModel
    {
        public int OrderId { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }
    }
}