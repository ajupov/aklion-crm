namespace Aklion.Crm.Domain.ProductAttribute
{
    public class ProductAttributeModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}