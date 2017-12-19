using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Product
{
    [WhereCombination("and")]
    public class ProductAutocompleteParameterModel
    {
        [Where("@StoreId is null or p.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or p.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or p.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}