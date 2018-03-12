using Infrastructure.Dao.Attributes;
using Infrastructure.Dao.Enums;
using Infrastructure.Dao.Models;

namespace Crm.Domain.Client
{
    public class ShortClientParameterModel
    {
        [Where("@c.StoreId = @StoreId")]
        public int StoreId { get; set; }

        [Filter("c")]
        public FilterModel Id { get; set; }

        [Filter("c")]
        public FilterModel Name { get; set; }

        [Filter("c")]
        public FilterModel IsDeleted { get; set; }

        [Filter("c")]
        public FilterModel CreateDate { get; set; }

        [FilterCombination]
        public FilterCombination FilterCombination { get; set; }

        [SortingColumn]
        public string SortingColumn { get; set; }

        [SortingOrder]
        public string SortingOrder { get; set; }

        [Page]
        public int? Page { get; set; }

        [Size]
        public int? Size { get; set; }
    }
}