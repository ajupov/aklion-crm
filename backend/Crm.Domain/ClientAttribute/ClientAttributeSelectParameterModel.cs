using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ClientAttribute
{
    [WhereCombination("and")]
    public class ClientAttributeSelectParameterModel
    {
        [Where("coalesce(@StoreId, 0) = 0 or ca.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Where("ca.IsDeleted = 0")]
        public bool IsDeleted { get; set; }
    }
}