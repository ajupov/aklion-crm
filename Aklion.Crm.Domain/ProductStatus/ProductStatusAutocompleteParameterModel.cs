using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductStatus
{
    [WhereCombination("and")]
    public class ProductStatusAutocompleteParameterModel
    {
        [Where("ps.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ps.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}