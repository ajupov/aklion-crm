using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderSource
{
    [WhereCombination("and")]
    public class OrderSourceAutocompleteParameterModel
    {
        [Where("oso.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("oso.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}