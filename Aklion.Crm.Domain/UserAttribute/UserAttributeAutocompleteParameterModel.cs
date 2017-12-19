using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserAttribute
{
    [WhereCombination("and")]
    public class UserAttributeAutocompleteParameterModel
    {
        [Where("@StoreId is null or ua.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Description is null or ua.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("@IsDeleted is null or ua.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}