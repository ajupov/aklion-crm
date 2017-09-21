using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.Dao.Helpers;
using Aklion.Infrastructure.Storage.DataBaseExecutor;

namespace Aklion.Crm.Dao
{
    public class Dao : IDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public Dao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<TModel> Get<TModel>(int id)
        {
            var type = typeof(TModel);
            var table = type.Name;
            var columns = type.GetProperties().Select(x => x.Name).ToList();
            var joinedColumns = string.Join(", ", columns.Select(x => $"[{x}]"));

            var query = $"select top 1 {joinedColumns} from [dbo].[{table}];";
            return _dataBaseExecutor.SelectOne<TModel>(query, new {id});
        }

        public Task<int> GetCount<TModel>(object parameters)
        {
            var type = typeof(TModel);
            var table = type.Name;
            var pairs = parameters.GetType().GetProperties().ToDictionary(k => k.Name, v => v.GetValue(parameters));

            var filter = DaoHelper.GetFilter(pairs);
            var query = $"select count(0) from [dbo].[{table}] " +
                        $"{filter}";

            return _dataBaseExecutor.SelectOne<int>(query, parameters);
        }

        public Task<List<TModel>> GetList<TModel>(object parameters)
        {
            var type = typeof(TModel);
            var table = type.Name;
            var columns = type.GetProperties().Select(x => x.Name).ToList();
            var joinedColumns = string.Join(", ", columns.Select(x => $"[{x}]"));
            var pairs = parameters.GetType().GetProperties().ToDictionary(k => k.Name, v => v.GetValue(parameters));

            var filter = DaoHelper.GetFilter(pairs);
            var sorting = DaoHelper.GetSorting(pairs, columns);
            var paging = DaoHelper.GetPaging(pairs);

            var query = $"select {joinedColumns} from [dbo].[{table}] " +
                        $"{filter}" +
                        $"order by [{sorting.Name}] {sorting.Order} " +
                        $"offset {paging.Page * paging.Rows} rows " +
                        $"fetch next {paging.Rows} rows only";

            return _dataBaseExecutor.SelectList<TModel>(query, parameters);
        }

        public Task<int> Create<TModel>(TModel model)
        {
            var type = typeof(TModel);
            var table = type.Name;
            var columns = type.GetProperties().Select(x => x.Name).ToList();
            var joinedColumns = string.Join(", ", columns.Where(x => x != "Id").Select(x => $"[{x}]"));
            var joinedValues = string.Join(", ", columns.Where(x => x != "Id").Select(x => $"@{x}"));

            var query = $@"insert [dbo].[{table}] ({joinedColumns}) values ({joinedValues}); select scope_identity();";
            return _dataBaseExecutor.SelectOne<int>(query, model);
        }

        public Task Update<TModel>(TModel model)
        {
            var type = typeof(TModel);
            var table = type.Name;
            var columns = type.GetProperties().Select(x => x.Name).ToList();
            var joinedPairs = string.Join(", ", columns.Where(x => x != "Id").Select(x => $"[{x}] = @{x}"));

            var query = $"update [dbo].[{table}] set {joinedPairs} where [Id] = @Id;";
            return _dataBaseExecutor.Execute(query, model);
        }

        public Task Delete<TModel>(int id)
        {
            var type = typeof(TModel);
            var table = type.Name;

            var query = $"delete from [dbo].[{table}] where [Id] = @id;";
            return _dataBaseExecutor.Execute(query, new {id});
        }
    }
}