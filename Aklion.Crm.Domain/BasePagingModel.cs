using System.Collections.Generic;

namespace Aklion.Crm.Domain
{
    public class BasePagingModel<T>
    {
        public int TotalCount { get; set; }

        public List<T> List { get; set; }
    }
}