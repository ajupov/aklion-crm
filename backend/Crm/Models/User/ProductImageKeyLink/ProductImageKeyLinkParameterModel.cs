using Infrastructure.Dao.Models;

namespace Crm.Models.User.ProductImageKeyLink
{
    public class ProductImageKeyLinkParameterModel : BaseParameterModel
    {
        public int? ProductId { get; set; }

        public int? KeyId { get; set; }

        public string KeyKey { get; set; }

        public string KeyName { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }
    }
}