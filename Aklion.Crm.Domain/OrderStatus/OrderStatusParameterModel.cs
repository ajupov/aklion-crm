namespace Aklion.Crm.Domain.OrderStatus
{
    public class OrderStatusParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }
    }
}