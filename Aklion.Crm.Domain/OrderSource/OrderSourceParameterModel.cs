namespace Aklion.Crm.Domain.OrderSource
{
    public class OrderSourceParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }
    }
}