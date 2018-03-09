namespace Crm.Models.Administration.Order
{
    public class OrderParameterModel
    {
        public int? Id { get; set; }

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

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}