using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ClientAttribute
{
    [WhereCombination("and")]
    public class ClientAttributeAutocompleteParameterModel
    {
        [Where("@StoreId is null or ca.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Description is null or ca.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("@IsDeleted is null or ca.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}