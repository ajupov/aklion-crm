namespace Aklion.Crm.Domain.Order
{
    public class OrderModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int ClientId { get; set; }

        public string ClientName { get; }

        public int SourceId { get; set; }

        public string SourceName { get; }

        public int StatusId { get; set; }

        public string StatusName { get; }

        public decimal TotalSum { get; set; }

        public decimal DiscountSum { get; set; }

        public bool IsDeleted { get; set; }
    }
}