using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.Post
{
    public interface IPostDao
    {
        Task<Paging<PostModel>> GetPagedList(PostParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId);

        Task<PostModel> Get(int id);

        Task<int> Create(PostModel model);

        Task Update(PostModel model);

        Task Delete(int id);
    }
}