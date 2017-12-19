using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderSource
{
    [WhereCombination("and")]
    public class OrderSourceAutocompleteParameterModel
    {
        [Where("@StoreId is null or oso.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or oso.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}