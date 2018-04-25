using Infrastructure.Dao.Models;

namespace Crm.Models.User.UserAttributeLink
{
    public class UserAttributeLinkParameterModel : BaseParameterModel
    {
        public int UserId { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }
    }
}