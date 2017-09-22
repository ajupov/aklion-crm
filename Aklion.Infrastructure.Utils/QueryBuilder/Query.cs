using System;
using System.Collections.Generic;

namespace Aklion.Infrastructure.Utils.QueryBuilder
{
    public class Query
    {
        public Type Type { get; set; }

        public string Table { get; set; }

        public List<string> Columns { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public string General { get; set; }

        public string Filter { get; set; }

        public string Sorting { get; set; }

        public string Paging { get; set; }
    }
}