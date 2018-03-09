using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserAttribute
{
    [WhereCombination("and")]
    public class UserAttributeAutocompleteParameterModel
    {
        [Where("ua.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ua.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("ua.IsDeleted = @IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}