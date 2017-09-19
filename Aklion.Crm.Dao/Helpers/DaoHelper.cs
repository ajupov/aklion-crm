using System;
using System.Collections.Generic;
using System.Linq;

namespace Aklion.Crm.Dao.Helpers
{
    public class DaoHelper
    {
        public static string GetFilter(Dictionary<string, object> pairs)
        {
            var result = string.Empty;

            if (!pairs.Any())
            {
                return result;
            }

            var searchPair = pairs.FirstOrDefault(x => x.Key == "_search");
            if (searchPair.Value.ToString().ToLower() != true.ToString().ToLower())
            {
                return result;
            }

            var exeptedNames = new List<string> { "_search", "page", "rows", "sidx", "sord" };
            var conditions = new List<string>();

            foreach (var pair in pairs.Where(x => !exeptedNames.Contains(x.Key)))
            {
                if (pair.Value == null)
                {
                    continue;
                }

                var typeName = pair.Value.GetType();

                if (typeName == typeof(bool) || typeName == typeof(int) || typeName == typeof(decimal) ||
                    typeName == typeof(float) || typeName == typeof(double) || typeName == typeof(DateTime))
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
                return result;
            }

            result = $"where {string.Join(" and ", conditions)}";

            return result;
        }

        public static PagingParameter GetPaging(IReadOnlyDictionary<string, object> filters)
        {
            var result = new PagingParameter
            {
                Page = 0,
                Rows = int.MaxValue
            };

            if (!filters.Any())
            {
                return result;
            }

            if (!filters.ContainsKey("page") || !filters.ContainsKey("rows"))
            {
                return result;
            }

            var pageString = filters["page"].ToString();
            var rowsString = filters["rows"].ToString();
            if (string.IsNullOrWhiteSpace(pageString) || string.IsNullOrWhiteSpace(rowsString))
            {
                return result;
            }

            if (!int.TryParse(pageString, out var page) || !int.TryParse(rowsString, out var rows))
            {
                return result;
            }

            if (page <= 0 || rows <= 0)
            {
                return result;
            }

            result.Page = page - 1;
            result.Rows = rows;

            return result;
        }

        public static SortingParameter GetSorting(IReadOnlyDictionary<string, object> filters,
            ICollection<string> columns)
        {
            var hasCreateDateColumn = columns.Any(x => x == "CreateDate");
            var hasNameColumn = columns.Any(x => x == "Name");

            var result = new SortingParameter
            {
                Name = hasCreateDateColumn ? "CreateDate" : hasNameColumn ? "Name" : "Id",
                Order = hasCreateDateColumn ? "desc" : hasNameColumn ? "asc" : "desc"
            };

            if (!filters.ContainsKey("sidx"))
            {
                return result;
            }

            var sidx = filters["sidx"]?.ToString();
            if (string.IsNullOrWhiteSpace(sidx))
            {
                return result;
            }

            if (!columns.Contains(sidx))
            {
                return result;
            }

            result.Name = sidx;

            if (!filters.ContainsKey("sord"))
            {
                return result;
            }

            var sord = filters["sord"]?.ToString();
            result.Order = sord == "desc" ? "desc" : "asc";

            return result;
        }
    }
}