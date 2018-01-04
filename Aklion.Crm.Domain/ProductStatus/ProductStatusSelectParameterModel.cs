using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductStatus
{
    public class ProductStatusSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or ps.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}