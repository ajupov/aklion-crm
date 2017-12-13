namespace Aklion.Crm.Domain.ProductAttribute
{
    public class ProductAttributeParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}