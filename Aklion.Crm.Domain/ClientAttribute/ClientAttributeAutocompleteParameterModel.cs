using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ClientAttribute
{
    [WhereCombination("and")]
    public class ClientAttributeAutocompleteParameterModel
    {
        [Where("ca.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ca.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("ca.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}