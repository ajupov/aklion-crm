using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderSource
{
    public class OrderSourceSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or oso.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}