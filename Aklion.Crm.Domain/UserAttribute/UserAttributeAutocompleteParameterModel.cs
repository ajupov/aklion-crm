using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserAttribute
{
    [WhereCombination("and")]
    public class UserAttributeAutocompleteParameterModel
    {
        [Where("ua.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ua.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("ua.IsDeleted = @IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}