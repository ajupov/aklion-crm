using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Category;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Category
{
    public interface ICategoryDao
    {
        Task<Paging<CategoryModel>> GetPagedList(CategoryParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId);

        Task<CategoryModel> Get(int id);

        Task<int> Create(CategoryModel model);

        Task Update(CategoryModel model);

        Task Delete(int id);
    }
}