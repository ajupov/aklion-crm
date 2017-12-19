using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttribute
{
    [WhereCombination("and")]
    public class ProductAttributeAutocompleteParameterModel
    {
        [Where("pa.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("pa.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("pa.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}