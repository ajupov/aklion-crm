namespace Aklion.Crm.Domain.Order
{
    public class OrderParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ClientId { get; set; }

        public string ClientName { get; set; }

        public int? SourceId { get; set; }

        public string SourceName { get; set; }

        public int? StatusId { get; set; }

        public string StatusName { get; set; }

        public decimal? TotalSum { get; set; }

        public decimal? DiscountSum { get; set; }

        public bool? IsDeleted { get; set; }
    }
}