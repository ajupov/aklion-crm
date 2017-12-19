using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttribute
{
    [WhereCombination("and")]
    public class ProductAttributeAutocompleteParameterModel
    {
        [Where("@StoreId is null or pa.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Description is null or pa.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("@IsDeleted is null or pa.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}