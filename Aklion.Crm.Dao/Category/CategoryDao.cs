using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Category;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Category
{
    public class CategoryDao : ICategoryDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public CategoryDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<CategoryModel>> GetPagedList(CategoryParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<CategoryModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId)
        {
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern, storeId });
        }

        public Task<CategoryModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<CategoryModel>(Queries.Get, new {id});
        }

        public Task<int> Create(CategoryModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(CategoryModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}