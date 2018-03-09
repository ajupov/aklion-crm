using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Store
{
    [WhereCombination("and")]
    public class StoreAutocompleteParameterModel
    {
        [Where("s.Name like @Name + '%'")]
        public string Name { get; set; }
    }
}