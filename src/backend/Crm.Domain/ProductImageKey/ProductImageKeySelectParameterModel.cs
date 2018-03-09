using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ProductImageKey
{
    public class ProductImageKeySelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or pik.StoreId = @StoreId")]
        public int StoreId { get; set; }
    }
}