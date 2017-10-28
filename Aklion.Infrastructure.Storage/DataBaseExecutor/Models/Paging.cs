using System.Collections.Generic;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor.Models
{
    public class Paging<T>
    {
        public int TotalCount { get; set; }

        public List<T> List { get; set; }
    }
}