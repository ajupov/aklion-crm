namespace Crm.Storages.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public string Name { get; set; }
    }
}