using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductStatus
{
    [WhereCombination("and")]
    public class ProductStatusAutocompleteParameterModel
    {
        [Where("@StoreId is null or ps.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or ps.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}