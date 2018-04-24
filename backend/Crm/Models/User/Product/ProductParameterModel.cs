using System.Collections.Generic;
using Infrastructure.Dao.Models;

namespace Crm.Models.User.Product
{
    public class ProductParameterModel : BaseParameterModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? ParentProductId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int? StatusId { get; set; }

        public string VendorCode { get; set; }

        public bool? IsDeleted { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }

        public Dictionary<int, string> Attributes { get; set; }
    }
}