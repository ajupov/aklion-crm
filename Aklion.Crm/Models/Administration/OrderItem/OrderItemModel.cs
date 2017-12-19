namespace Aklion.Crm.Models.Administration.OrderItem
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}