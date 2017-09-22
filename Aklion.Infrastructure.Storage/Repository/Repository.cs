using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Utils.QueryBuilder;

namespace Aklion.Infrastructure.Storage.Repository
{
    public class Repository : IRepository
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public Repository(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<TModel> Get<TModel>(int id)
        {
            var script = QueryBuilder
                .CreateFor<TModel>()
                .AddColumns()
                .AddSelectTop1()
                .Build();

            return _dataBaseExecutor.SelectOne<TModel>(script, new { id });
        }

        public async Task<(int, List<TModel>)> Get<TModel>(object parameters)
        {
            var baseQuery = QueryBuilder
                .CreateFor<TModel>()
                .AddColumns()
                .AddParameters(parameters)
                .AddFilter();

            var countScript = baseQuery
                .AddSelectCount()
                .Build();

            var count = await _dataBaseExecutor.SelectOne<int>(countScript, parameters).ConfigureAwait(false);

            var listScript = baseQuery
                .AddSelectList()
                .AddSorting()
                .AddPaging()
                .Build();

            var list = await _dataBaseExecutor.SelectList<TModel>(listScript, parameters).ConfigureAwait(false);

            return (count, list);
        }

        public Task<int> Create<TModel>(TModel model)
        {
            var script = QueryBuilder
                .CreateFor<TModel>()
                .AddColumns()
                .AddInsert()
                .Build();

            return _dataBaseExecutor.SelectOne<int>(script, model);
        }

        public Task Update<TModel>(TModel model)
        {
            var script = QueryBuilder
                .CreateFor<TModel>()
                .AddColumns()
                .AddUpdate()
                .Build();

            return _dataBaseExecutor.Execute(script, model);
        }

        public Task Delete<TModel>(int id)
        {
            var script = QueryBuilder
                .CreateFor<TModel>()
                .AddDelete()
                .Build();

            return _dataBaseExecutor.Execute(script, new { id });
        }
    }
}