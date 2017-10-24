using System.Collections.Generic;

namespace Aklion.Crm.Domain.Models
{
    public class BaseListModel<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}