using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderAttribute
{
    [WhereCombination("and")]
    public class OrderAttributeAutocompleteParameterModel
    {
        [Where("@StoreId is null or oa.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Description is null or oa.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("@IsDeleted is null or oa.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}