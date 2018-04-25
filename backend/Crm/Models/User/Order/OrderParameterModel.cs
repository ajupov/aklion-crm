using System.Collections.Generic;
using Infrastructure.Dao.Models;

namespace Crm.Models.User.Order
{
    public class OrderParameterModel : BaseParameterModel
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public string ClientName { get; set; }

        public int? SourceId { get; set; }

        public string SourceName { get; set; }

        public int? StatusId { get; set; }

        public string StatusName { get; set; }

        public decimal? MinTotalSum { get; set; }

        public decimal? MaxTotalSum { get; set; }

        public decimal? MinDiscountSum { get; set; }

        public decimal? MaxDiscountSum { get; set; }

        public bool? IsDeleted { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }

        public Dictionary<int, string> Attributes { get; set; }
    }
}