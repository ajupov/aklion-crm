namespace Crm.Models.User.OrderItem
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}