using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductImageKey
{
    public class ProductImageKeySelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or pik.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}