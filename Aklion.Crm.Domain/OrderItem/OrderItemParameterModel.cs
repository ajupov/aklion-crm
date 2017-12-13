namespace Aklion.Crm.Domain.OrderItem
{
    public class OrderItemParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public int? Count { get; set; }

        public bool? IsDeleted { get; set; }
    }
}