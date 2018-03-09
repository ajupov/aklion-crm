using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ProductAttribute
{
    [WhereCombination("and")]
    public class ProductAttributeAutocompleteParameterModel
    {
        [Where("pa.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("pa.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("pa.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}