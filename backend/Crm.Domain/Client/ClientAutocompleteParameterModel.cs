using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Client
{
    [WhereCombination("and")]
    public class ClientAutocompleteParameterModel
    {
        [Where("c.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("c.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("c.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}