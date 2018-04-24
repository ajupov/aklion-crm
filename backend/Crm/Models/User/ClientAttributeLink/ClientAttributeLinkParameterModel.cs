using Infrastructure.Dao.Models;

namespace Crm.Models.User.ClientAttributeLink
{
    public class ClientAttributeLinkParameterModel : BaseParameterModel
    {
        public int ClientId { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }
    }
}