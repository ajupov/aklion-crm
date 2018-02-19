using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Query;
using Aklion.Infrastructure.Query.Enums;

namespace Aklion.Infrastructure.Dao
{
    public class Dao : IDao
    {
        private readonly IDataBaseExecutor _executor;

        public Dao(IDataBaseExecutor executor)
        {
            _executor = executor;
        }

        public Task<TModel> GetAsync<TModel>(int id)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectOne)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyIdFilter()
                .Build();

            return _executor.SelectOneAsync<TModel>(query, new {id});
        }

        public Task<TModel> GetAsync<TModel, TParameter>(TParameter parameter)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectOne)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyFilter(parameter)
                .Build();

            return _executor.SelectOneAsync<TModel>(query, parameter);
        }

        public Task<List<TModel>> GetListAsync<TModel>(bool distinct = false)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectList, distinct)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .Build();

            return _executor.SelectListAsync<TModel>(query);
        }

        public Task<List<TModel>> GetListAsync<TModel, TParameter>(TParameter parameter, bool distinct = false)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectList, distinct)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyFilter(parameter)
                .Build();

            return _executor.SelectListAsync<TModel>(query, parameter);
        }

        public Task<(int TotalCount, List<TModel> List)> GetPagedListAsync<TModel, TParameter>(TParameter parameter,
            bool distinct = false)
        {
            var query1 = QueryBuilder
                .Create<TModel>(QueryType.SelectCount)
                .DefineTableName()
                .ApplyJoins()
                .ApplyFilter(parameter)
                .Build();

            var query2 = QueryBuilder
                .Create<TModel>(QueryType.SelectPagedList, distinct)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyFilter(parameter)
                .ApplySorting(parameter)
                .ApplyPaging(parameter)
                .Build();

            return _executor.SelectMultipleAsync(query1 + query2, async r => (
                await r.SelectOneAsync<int>().ConfigureAwait(false),
                await r.SelectListAsync<TModel>().ConfigureAwait(false)
                ), parameter);
        }

        public Task<Dictionary<string, int>> GetForAutoCompleteAsync<TModel, TParameter>(TParameter parameter,
            bool distinct = false)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectForAutocompleteOrSelect, distinct)
                .DefineTableName()
                .DefineColumnsForAutocompleteOrSelect()
                .ApplyFilter(parameter)
                .Build();

            return _executor.SelectDictonaryAsync(query, parameter);
        }

        public Task<Dictionary<string, int>> GetForSelectAsync<TModel, TParameter>(TParameter parameter,
            bool distinct = false)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectForAutocompleteOrSelect, distinct)
                .DefineTableName()
                .DefineColumnsForAutocompleteOrSelect()
                .ApplyFilter(parameter)
                .Build();

            return _executor.SelectDictonaryAsync(query, parameter);
        }

        public Task<int> CreateAsync<TModel>(TModel model)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Insert)
                .DefineTableName()
                .DefineColumnsForInsert()
                .Build();

            return _executor.SelectOneAsync<int>(query, model);
        }

        public Task CreateListAsync<TModel>(List<TModel> model)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.InsertList)
                .DefineTableName()
                .DefineColumnsForInsert()
                .Build();

            return _executor.ExecuteAsync(query, model);
        }

        public Task UpdateAsync<TModel>(TModel model)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Update)
                .DefineTableName()
                .DefineColumnsForUpdate()
                .ApplyIdFilter()
                .Build();

            return _executor.ExecuteAsync(query, model);
        }

        public Task DeleteAsync<TModel>(int id)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Delete)
                .DefineTableName()
                .ApplyIdFilter()
                .Build();

            return _executor.ExecuteAsync(query, new {id});
        }
    }
}