namespace Aklion.Crm.Domain.ProductAttributeLink
{
    public class ProductAttributeLinkParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }
    }
}