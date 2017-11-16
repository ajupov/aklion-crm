using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Tag;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Tag
{
    public interface ITagDao
    {
        Task<Paging<TagModel>> GetPagedList(TagParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId);

        Task<TagModel> Get(int id);

        Task<int> Create(TagModel model);

        Task Update(TagModel model);

        Task Delete(int id);
    }
}