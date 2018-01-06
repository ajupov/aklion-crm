﻿using System.Linq;
using System.Reflection;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Infrastructure.Query
{
    public static class QueryBuilder
    {
        public static QueryObject Create<TModel>(QueryType queryType)
        {
            return new QueryObject(queryType)
            {
                Type = typeof(TModel),
                Properties = typeof(TModel).GetProperties().ToList()
            };
        }

        public static QueryObject DefineTableName(this QueryObject queryObject)
        {
            queryObject.TableName = ((TableAttribute) queryObject.Type.GetCustomAttribute(typeof(TableAttribute))).Name;
            queryObject.TableNameWithoutAlias = queryObject.TableName.Split(' ').FirstOrDefault();
            queryObject.TableAlias = queryObject.TableName.Split(" as ").LastOrDefault();

            if (queryObject.TableAlias == queryObject.TableNameWithoutAlias)
            {
                queryObject.TableAlias = string.Empty;
            }

            return queryObject;
        }

        public static QueryObject ApplyJoins(this QueryObject queryObject)
        {
            queryObject.Joins = string.Join(' ',
                queryObject.Type.GetCustomAttributes(typeof(JoinAttribute)).Cast<JoinAttribute>().Select(a => a.Value));

            return queryObject;
        }

        public static QueryObject DefineColumnsForSelect(this QueryObject queryObject)
        {
            queryObject.ColumnsForSelect = string.Join(", ", queryObject.Properties
                .Where(p => p.GetCustomAttribute(typeof(ColumnAttribute)) != null)
                .Select(a => $"{((ColumnAttribute) a.GetCustomAttribute(typeof(ColumnAttribute))).Value} as {a.Name}"));

            return queryObject;
        }

        public static QueryObject DefineColumnsForAutocompleteOrSelect(this QueryObject queryObject)
        {
            var identificatorColumnName = ((ColumnAttribute) queryObject.Properties.FirstOrDefault(p =>
                    p.GetCustomAttribute(typeof(IdentificatorAttribute)) != null &&
                    p.GetCustomAttribute(typeof(ColumnAttribute)) != null).GetCustomAttribute(typeof(ColumnAttribute)))
                .Value;

            if (string.IsNullOrWhiteSpace(identificatorColumnName))
            {
                return queryObject;
            }

            var autocompleteColumnName = ((AutocompleteOrSelectAttribute) queryObject.Properties.FirstOrDefault(p =>
                    p.GetCustomAttribute(typeof(AutocompleteOrSelectAttribute)) != null)
                .GetCustomAttribute(typeof(AutocompleteOrSelectAttribute))).Value;

            if (string.IsNullOrWhiteSpace(autocompleteColumnName))
            {
                return queryObject;
            }

            queryObject.ColumnsForAutocomplete = $"{autocompleteColumnName} as Value, {identificatorColumnName} as [Key]";

            return queryObject;
        }

        public static QueryObject DefineColumnsForInsert(this QueryObject queryObject)
        {
            queryObject.ColumnsForInsert = string.Join(", ", queryObject.Properties
                .Where(p =>
                {
                    var columnAttribute = p.GetCustomAttribute(typeof(ColumnAttribute));
                    if (columnAttribute == null)
                    {
                        return false;
                    }

                    var identificatorAttribute = p.GetCustomAttribute(typeof(IdentificatorAttribute));
                    if (identificatorAttribute != null)
                    {
                        return false;
                    }

                    var columnAttributeValue = ((ColumnAttribute) columnAttribute).Value;
                    if(string.IsNullOrWhiteSpace(columnAttributeValue))
                    {
                        return false;
                    }

                    var columnAttributeValueAlias = columnAttributeValue.Split('.').FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(columnAttributeValueAlias))
                    {
                        return true;
                    }

                    return columnAttributeValueAlias == queryObject.TableAlias;
                })
                .Select(a => $"@{((ColumnAttribute) a.GetCustomAttribute(typeof(ColumnAttribute))).Value.Replace(queryObject.TableAlias + ".", string.Empty)}"));

            return queryObject;
        }

        public static QueryObject DefineColumnsForUpdate(this QueryObject queryObject)
        {
            queryObject.ColumnsForUpdate =
                string.Join(", ", queryObject.Properties
                .Where(p =>
                    {
                        var columnAttribute = p.GetCustomAttribute(typeof(ColumnAttribute));
                        if (columnAttribute == null)
                        {
                            return false;
                        }

                        var identificatorAttribute = p.GetCustomAttribute(typeof(IdentificatorAttribute));
                        if (identificatorAttribute != null)
                        {
                            return false;
                        }

                        var createDateAttribute = p.GetCustomAttribute(typeof(CreateDateAttribute));
                        if (createDateAttribute != null)
                        {
                            return false;
                        }

                        var columnAttributeValue = ((ColumnAttribute)columnAttribute).Value;
                        if (string.IsNullOrWhiteSpace(columnAttributeValue))
                        {
                            return false;
                        }

                        var columnAttributeValueAlias = columnAttributeValue.Split('.').FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(columnAttributeValueAlias))
                        {
                            return true;
                        }

                        return columnAttributeValueAlias == queryObject.TableAlias;
                    })

                    .Select(a =>
                    {
                        var name = ((ColumnAttribute) a.GetCustomAttribute(typeof(ColumnAttribute))).Value.Replace(
                            queryObject.TableAlias + ".", string.Empty);

                        return $"{name} = @{name}";
                    }));

            return queryObject;
        }

        public static QueryObject ApplyFilter<TParameter>(this QueryObject queryObject, TParameter parameter)
        {
            queryObject.FilterType = typeof(TParameter);
            queryObject.FilterProperties = queryObject.FilterType.GetProperties().ToList();

            var filterGroupAttribute =
                (WhereCombinationAttribute)queryObject.FilterType.GetCustomAttribute(typeof(WhereCombinationAttribute));

            var propertiesWithWhereAttributes =
                queryObject.FilterProperties.Where(p => p.GetCustomAttribute(typeof(WhereAttribute)) != null).ToList();

            var filters = propertiesWithWhereAttributes
                .Select(p => $"({((WhereAttribute)p.GetCustomAttribute(typeof(WhereAttribute))).Value})")
                .ToList();

            if (filters.Count == 1)
            {
                queryObject.Filters = $"where {filters.FirstOrDefault()}";
            }
            else if (filters.Count > 1)
            {
                var group = !string.IsNullOrWhiteSpace(filterGroupAttribute.Value) ? filterGroupAttribute.Value : "and";
                queryObject.Filters = $"where {string.Join($" {@group} ", filters)}";
            }

            return queryObject;
        }

        public static QueryObject ApplyIdFilter(this QueryObject queryObject)
        {
            queryObject.Filters = $"where {queryObject.TableAlias}.Id = @id";

            return queryObject;
        }

        public static QueryObject ApplySorting<TParameterModel>(this QueryObject queryObject, TParameterModel parameter)
        {
            var propertiesWithSortingColumnAttribute =
                queryObject.FilterProperties.FirstOrDefault(p => p.GetCustomAttribute(typeof(SortingColumnAttribute)) != null);

            var propertiesWithSortingOrderAttribute =
                queryObject.FilterProperties.FirstOrDefault(p => p.GetCustomAttribute(typeof(SortingOrderAttribute)) != null);

            if (propertiesWithSortingColumnAttribute != null && propertiesWithSortingOrderAttribute != null)
            {
                var sortingColumn = propertiesWithSortingColumnAttribute.GetValue(parameter).ToString();
                var sortingOrder = propertiesWithSortingOrderAttribute.GetValue(parameter).ToString();

                if (!string.IsNullOrWhiteSpace(sortingColumn))
                {
                    var sortingColumnWithAlias = string.Empty;

                    foreach (var property in queryObject.Properties)
                    {
                        var columnAttribute = (ColumnAttribute) property.GetCustomAttribute(typeof(ColumnAttribute));
                        if (columnAttribute == null)
                        {
                            continue;
                        }

                        if (property.Name != sortingColumn)
                        {
                            continue;
                        }

                        sortingColumnWithAlias = columnAttribute.Value;
                        break;
                    }

                    queryObject.Sorting = $"order by {sortingColumnWithAlias} " +
                                          (sortingOrder == "desc" ? "desc" : "asc");
                }
            }

            return queryObject;
        }

        public static QueryObject ApplyPaging<TParameterModel>(this QueryObject queryObject, TParameterModel parameter)
        {
            var propertiesWithPageAttribute =
                queryObject.FilterProperties.FirstOrDefault(p => p.GetCustomAttribute(typeof(PageAttribute)) != null);

            var propertiesWithSizeAttribute =
                queryObject.FilterProperties.FirstOrDefault(p => p.GetCustomAttribute(typeof(SizeAttribute)) != null);

            if (propertiesWithPageAttribute != null && propertiesWithSizeAttribute != null)
            {
                var pageObject = propertiesWithPageAttribute.GetValue(parameter);
                var sizeObject = propertiesWithSizeAttribute.GetValue(parameter);

                var page = pageObject != null ? int.Parse(pageObject.ToString()) : 0;
                var size = sizeObject != null ? int.Parse(sizeObject.ToString()) : 0;

                if (page <= 0)
                {
                    page = 0;
                }

                page--;

                if (size > 0)
                {
                    var offset = page * size;

                    queryObject.Paging = $"offset {offset} rows fetch next {size} rows only";
                }
            }

            return queryObject;
        }

        public static string Build(this QueryObject queryObject)
        {
            const string noLockSelectCommand = "set nocount on; set transaction isolation level read uncommitted;";

            switch (queryObject.QueryType)
            {
                case QueryType.SelectCount:
                    return $"{noLockSelectCommand} select count(0) from {queryObject.TableName} {queryObject.Joins} {queryObject.Filters};";
                case QueryType.SelectOne:
                    return $"{noLockSelectCommand} select top 1 {queryObject.ColumnsForSelect} from {queryObject.TableName} {queryObject.Joins} {queryObject.Filters};";
                case QueryType.SelectList:
                    return $"{noLockSelectCommand} select {queryObject.ColumnsForSelect} from {queryObject.TableName} {queryObject.Joins} {queryObject.Filters};";
                case QueryType.SelectPagedList:
                    return $"{noLockSelectCommand} select {queryObject.ColumnsForSelect} from {queryObject.TableName} {queryObject.Joins} {queryObject.Filters} {queryObject.Sorting} {queryObject.Paging};";
                case QueryType.SelectForAutocompleteOrSelect:
                    return $"{noLockSelectCommand} select {queryObject.ColumnsForAutocomplete} from {queryObject.TableName} {queryObject.Filters};";
                case QueryType.Insert:
                    return $"insert {queryObject.TableNameWithoutAlias} ({queryObject.ColumnsForInsert.Replace("@", string.Empty)}) values ({queryObject.ColumnsForInsert}); select scope_identity();";
                case QueryType.Update:
                    return $"update {queryObject.TableNameWithoutAlias} set {queryObject.ColumnsForUpdate} {queryObject.Filters.Replace($"{queryObject.TableAlias}.", string.Empty)}";
                case QueryType.Delete:
                    return $"delete from {queryObject.TableNameWithoutAlias} {queryObject.Filters.Replace($"{queryObject.TableAlias}.", string.Empty)};";
            }

            return string.Empty;
        }
    }
}