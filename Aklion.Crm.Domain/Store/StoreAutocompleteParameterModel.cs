using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Store
{
    [WhereCombination("and")]
    public class StoreAutocompleteParameterModel
    {
        [Where("s.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}