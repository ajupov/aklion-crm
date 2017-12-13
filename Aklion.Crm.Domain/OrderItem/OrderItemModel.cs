namespace Aklion.Crm.Domain.OrderItem
{
    public class OrderItemModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public bool IsDeleted { get; set; }
    }
}