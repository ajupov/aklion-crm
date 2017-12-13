namespace Aklion.Crm.Domain.OrderSource
{
    public class OrderSourceModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }
    }
}