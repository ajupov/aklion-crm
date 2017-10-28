namespace Aklion.Infrastructure.Storage.DataBaseExecutor
{
    public static class QueryHelper
    {
        public static string AddSorting(this string query, string columnName, string columnOrder)
        {
            query = query.TrimEnd(';');
            query += $"\r\norder by {columnName} {columnOrder};";

            return query;
        }

        public static string AddPaging(this string query, int page, int size)
        {
            query = query.TrimEnd(';');
            query += $"offset {(page * size > 0 ? page * size : 0)} rows";
            query += $"fetch next {(size > 0 ? size : int.MaxValue)} rows only;";

            return query;
        }
    }
}