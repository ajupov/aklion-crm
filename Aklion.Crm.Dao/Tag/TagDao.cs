using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Tag;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.Tag
{
    public class TagDao : ITagDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public TagDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<TagModel>> GetPagedList(TagParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<TagModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId)
        {
            return _dataBaseExecutor.SelectListAsync<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern, storeId });
        }

        public Task<TagModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<TagModel>(Queries.Get, new {id});
        }

        public Task<int> Create(TagModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task Update(TagModel model)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Delete, new {id});
        }
    }
}