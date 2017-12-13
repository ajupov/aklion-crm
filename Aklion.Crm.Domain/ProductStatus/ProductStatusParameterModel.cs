namespace Aklion.Crm.Domain.ProductStatus
{
    public class ProductStatusParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }
    }
}