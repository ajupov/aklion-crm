using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Store
{
    [WhereCombination("and")]
    public class StoreAutocompleteParameterModel
    {
        [Where("@Name is null or s.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}