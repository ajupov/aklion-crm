using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderStatus
{
    public class OrderStatusSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or ost.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}