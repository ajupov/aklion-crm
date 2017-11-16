using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Tag;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

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
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern, storeId });
        }

        public Task<TagModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<TagModel>(Queries.Get, new {id});
        }

        public Task<int> Create(TagModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(TagModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}