using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ProductStatus
{
    public class ProductStatusSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or ps.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}