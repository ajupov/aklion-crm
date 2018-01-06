using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Query;

namespace Aklion.Infrastructure.Dao
{
    public class Dao : IDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public Dao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
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

            return _dataBaseExecutor.SelectOneAsync<TModel>(query, new {id});
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
            
            return _dataBaseExecutor.SelectOneAsync<TModel>(query, parameter);
        }

        public Task<List<TModel>> GetListAsync<TModel>()
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectList)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .Build();

            return _dataBaseExecutor.SelectListAsync<TModel>(query);
        }

        public Task<List<TModel>> GetListAsync<TModel, TParameter>(TParameter parameter)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectList)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyFilter(parameter)
                .Build();

            return _dataBaseExecutor.SelectListAsync<TModel>(query, parameter);
        }

        public Task<Tuple<int, List<TModel>>> GetPagedListAsync<TModel, TParameter>(TParameter parameter)
        {
            var query1 = QueryBuilder
                .Create<TModel>(QueryType.SelectCount)
                .DefineTableName()
                .ApplyJoins()
                .ApplyFilter(parameter)
                .Build();

            var query2 = QueryBuilder
                .Create<TModel>(QueryType.SelectPagedList)
                .DefineTableName()
                .ApplyJoins()
                .DefineColumnsForSelect()
                .ApplyFilter(parameter)
                .ApplySorting(parameter)
                .ApplyPaging(parameter)
                .Build();

            return _dataBaseExecutor.SelectMultipleAsync(query1 + query2, async r => new Tuple<int, List<TModel>>(
                await r.SelectOneAsync<int>().ConfigureAwait(false),
                await r.SelectListAsync<TModel>().ConfigureAwait(false)
            ), parameter);
        }

        public Task<Dictionary<string, int>> GetForAutoCompleteAsync<TModel, TParameter>(TParameter parameter)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectForAutocompleteOrSelect)
                .DefineTableName()
                .DefineColumnsForAutocompleteOrSelect()
                .ApplyFilter(parameter)
                .Build();

            return _dataBaseExecutor.SelectDictonaryAsync(query, parameter);
        }

        public Task<Dictionary<string, int>> GetForSelectAsync<TModel, TParameter>(TParameter parameter)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.SelectForAutocompleteOrSelect)
                .DefineTableName()
                .DefineColumnsForAutocompleteOrSelect()
                .ApplyFilter(parameter)
                .Build();

            return _dataBaseExecutor.SelectDictonaryAsync(query, parameter);
        }

        public Task<int> CreateAsync<TModel>(TModel model)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Insert)
                .DefineTableName()
                .DefineColumnsForInsert()
                .Build();

            return _dataBaseExecutor.SelectOneAsync<int>(query, model);
        }

        public Task<bool> UpdateAsync<TModel>(TModel model)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Update)
                .DefineTableName()
                .DefineColumnsForUpdate()
                .ApplyIdFilter()
                .Build();

            return _dataBaseExecutor.ExecuteAsync(query, model);
        }

        public Task<bool> DeleteAsync<TModel>(int id)
        {
            var query = QueryBuilder
                .Create<TModel>(QueryType.Delete)
                .DefineTableName()
                .ApplyIdFilter()
                .Build();

            return _dataBaseExecutor.ExecuteAsync(query, new {id});
        }
    }
}