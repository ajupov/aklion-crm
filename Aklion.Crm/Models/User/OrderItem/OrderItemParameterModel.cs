namespace Aklion.Crm.Models.User.OrderItem
{
    public class OrderItemParameterModel
    {
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public int? Count { get; set; }

        public string CreateDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}