namespace Aklion.Crm.Domain.OrderStatus
{
    public class OrderStatusModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }
    }
}