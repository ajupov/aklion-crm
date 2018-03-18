using Infrastructure.Dao.Models;

namespace Crm.Models.User.Order
{
    public class OrderParameterModel : BaseParameterModel
    {
        public FilterModel<int?> Id { get; set; }

        public FilterModel<int?> ClientId { get; set; }

        public FilterModel<string> ClientName { get; set; }

        public FilterModel<int?> SourceId { get; set; }

        public FilterModel<int?> StatusId { get; set; }

        public decimal? TotalSum { get; set; }

        public decimal? DiscountSum { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }
    }
}