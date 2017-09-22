using System.Collections.Generic;
using System.Linq;

namespace Aklion.Infrastructure.Utils.QueryBuilder
{
    public static class QueryBuilder
    {
        public static Query CreateFor<TModel>()
        {
            var type = typeof(TModel);

            return new Query
            {
                Type = type,
                Table = type.Name
            };
        }

        public static Query AddColumns(this Query query)
        {
            query.Columns = query.Type
                .GetProperties()
                .Select(x => x.Name)
                .ToList();

            return query;
        }

        public static Query AddParameters(this Query query, object parameters)
        {
            query.Parameters = parameters
                .GetType()
                .GetProperties()
                .ToDictionary(k => k.Name, v => v.GetValue(parameters));

            return query;
        }

        public static Query AddSelectCount(this Query query)
        {
            query.General = $"select count(0) from [dbo].[{query.Table}]";

            return query;
        }

        public static Query AddSelectTop1(this Query query)
        {
            var joinedColumns = string.Join(", ", query.Columns.Select(x => $"[{x}]"));
            query.General = $"select top 1 {joinedColumns} from [dbo].[{query.Table}];";

            return query;
        }

        public static Query AddSelectList(this Query query)
        {
            var joinedColumns = string.Join(", ", query.Columns.Select(x => $"[{x}]"));
            query.General = $"select {joinedColumns} from [dbo].[{query.Table}] ";

            return query;
        }

        public static Query AddFilter(this Query query)
        {
            if (!query.Parameters.Any())
            {
                return query;
            }

            var exeptedNames = new List<string> {"Page", "Size", "SortingColumn", "SortingOrder"};
            var conditions = new List<string>();

            foreach (var pair in query.Parameters.Where(x => !exeptedNames.Contains(x.Key)))
            {
                if (pair.Value == null)
                {
                    continue;
                }

                var typeName = pair.Value.GetType();

                if (typeName == typeof(bool) || typeName == typeof(int) || typeName == typeof(decimal) ||
                    typeName == typeof(float) || typeName == typeof(double) || typeName == typeof(System.DateTime))
                {
                    conditions.Add($"(@{pair.Key} is null or [{pair.Key}] = @{pair.Key})");
                }
                else if (typeName == typeof(string))
                {
                    conditions.Add($"(@{pair.Key} is null or [{pair.Key}] like @{pair.Key} + '%')");
                }
            }

            if (!conditions.Any())
            {
                return query;
            }

            query.Filter = $"where {string.Join(" and ", conditions)}";

            return query;
        }

        public static Query AddSorting(this Query query)
        {
            var hasCreateDateColumn = query.Columns.Any(x => x == "CreateDate");
            var hasNameColumn = query.Columns.Any(x => x == "Name");

            var name = hasCreateDateColumn ? "CreateDate" : hasNameColumn ? "Name" : "Id";
            var order = hasCreateDateColumn ? "desc" : hasNameColumn ? "asc" : "desc";

            if (!query.Parameters.ContainsKey("SortingColumn"))
            {
                query.Sorting = $"order by [{name}] {order}";

                return query;
            }

            var sortingColumn = query.Parameters["SortingColumn"]?.ToString();
            if (string.IsNullOrWhiteSpace(sortingColumn))
            {
                query.Sorting = $"order by [{name}] {order}";

                return query;
            }

            if (!query.Columns.Contains(sortingColumn))
            {
                query.Sorting = $"order by [{name}] {order}";

                return query;
            }

            name = sortingColumn;

            if (!query.Parameters.ContainsKey("SortingOrder"))
            {
                query.Sorting = $"order by [{name}] {order}";

                return query;
            }

            var sortingOrder = query.Parameters["SortingOrder"]?.ToString() == "descending" ? "desc" : "asc";
            query.Sorting = $"order by [{name}] {sortingOrder}";

            return query;
        }

        public static Query AddPaging(this Query query)
        {
            var page = 0;
            var size = int.MaxValue;

            if (!query.Parameters.Any())
            {
                query.Paging = $"offset {page * size} rows fetch next {size} rows only";

                return query;
            }

            if (!query.Parameters.ContainsKey("Page") || !query.Parameters.ContainsKey("Size"))
            {
                query.Paging = $"offset {page * size} rows fetch next {size} rows only";

                return query;
            }

            var pageString = query.Parameters["Page"].ToString();
            var rowsString = query.Parameters["Size"].ToString();
            if (string.IsNullOrWhiteSpace(pageString) || string.IsNullOrWhiteSpace(rowsString))
            {
                query.Paging = $"offset {page * size} rows fetch next {size} rows only";

                return query;
            }

            var sizeParameter = 0;
            if (!int.TryParse(pageString, out var pageParameter) || !int.TryParse(rowsString, out sizeParameter))
            {
                query.Paging = $"offset {page * size} rows fetch next {size} rows only";
            }

            if (pageParameter <= 0 || sizeParameter <= 0)
            {
                query.Paging = $"offset {page * size} rows fetch next {size} rows only";

                return query;
            }

            page = pageParameter - 1;
            size = sizeParameter;

            query.Paging = $"offset {page * size} rows fetch next {size} rows only";

            return query;
        }

        public static Query AddInsert(this Query query)
        {
            var joinedColumns = string.Join(", ", query.Columns.Where(x => x != "Id").Select(x => $"[{x}]"));
            var joinedValues = string.Join(", ", query.Columns.Where(x => x != "Id").Select(x => $"@{x}"));

            query.General = $"insert [dbo].[{query.Table}] ({joinedColumns}) " +
                            $"values ({joinedValues}); select scope_identity()";

            return query;
        }

        public static Query AddUpdate(this Query query)
        {
            var joinedPairs = string.Join(", ", query.Columns.Where(x => x != "Id").Select(x => $"[{x}] = @{x}"));

            query.General = $"update [dbo].[{query.Table}] set {joinedPairs} where [Id] = @Id;";

            return query;
        }

        public static Query AddDelete(this Query query)
        {
            query.General = $"delete from [dbo].[{query.Table}] where [Id] = @id;";

            return query;
        }

        public static string Build(this Query query)
        {
            return query.General + query.Filter + query.Sorting + query.Paging;
        }
    }
}