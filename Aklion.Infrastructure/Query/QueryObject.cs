using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aklion.Infrastructure.Query
{
    public class QueryObject
    {
        public QueryObject(QueryType queryType)
        {
            QueryType = queryType;
        }

        public QueryType QueryType { get; set; }

        public Type Type { get; set; }

        public Type FilterType { get; set; }

        public List<PropertyInfo> Properties { get; set; }

        public List<PropertyInfo> FilterProperties { get; set; }

        public string TableName { get; set; }

        public string TableAlias { get; set; }

        public string TableNameWithoutAlias { get; set; }

        public string Joins { get; set; }

        public string ColumnsForSelect { get; set; }

        public string ColumnsForAutocomplete { get; set; }

        public string ColumnsForInsert { get; set; }

        public string ColumnsForUpdate { get; set; }

        public string Filters { get; set; }

        public string Sorting { get; set; }
        
        public string Paging { get; set; }
    }
}