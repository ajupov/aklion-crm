using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderSource
{
    public class OrderSourceSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or oso.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}