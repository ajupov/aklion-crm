using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderStatus
{
    [WhereCombination("and")]
    public class OrderStatusAutocompleteParameterModel
    {
        [Where("@StoreId is null or ost.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or ost.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}