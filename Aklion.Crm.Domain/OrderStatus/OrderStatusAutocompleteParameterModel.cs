using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderStatus
{
    [WhereCombination("and")]
    public class OrderStatusAutocompleteParameterModel
    {
        [Where("ost.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ost.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}