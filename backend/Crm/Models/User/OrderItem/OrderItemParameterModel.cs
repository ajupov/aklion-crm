using Infrastructure.Dao.Models;

namespace Crm.Models.User.OrderItem
{
    public class OrderItemParameterModel : BaseParameterModel
    {
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int? MinCount { get; set; }

        public int? MaxCount { get; set; }

        public int? MinSum { get; set; }

        public int? MaxSum { get; set; }
    }
}