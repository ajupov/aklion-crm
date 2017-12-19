using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderAttribute
{
    [WhereCombination("and")]
    public class OrderAttributeAutocompleteParameterModel
    {
        [Where("oa.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("oa.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("oa.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}