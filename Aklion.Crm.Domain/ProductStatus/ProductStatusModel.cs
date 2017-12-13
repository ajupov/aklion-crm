namespace Aklion.Crm.Domain.ProductStatus
{
    public class ProductStatusModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }
    }
}