using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Product
{
    [WhereCombination("and")]
    public class ProductAutocompleteParameterModel
    {
        [Where("p.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("p.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("p.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}