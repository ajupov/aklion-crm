using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Client
{
    [WhereCombination("and")]
    public class ClientAutocompleteParameterModel
    {
        [Where("@StoreId is null or c.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or c.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or c.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}