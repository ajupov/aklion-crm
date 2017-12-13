using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.Attribute
{
    public interface IAttributeDao
    {
        Task<Paging<AttributeModel>> GetPagedList(AttributeParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId);

        Task<AttributeModel> Get(int id);

        Task<int> Create(AttributeModel model);

        Task Update(AttributeModel model);

        Task Delete(int id);
    }
}