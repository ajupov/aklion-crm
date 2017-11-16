using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Attribute;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Attribute
{
    public class AttributeDao : IAttributeDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public AttributeDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<AttributeModel>> GetPagedList(AttributeParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<AttributeModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId)
        {
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern, storeId });
        }

        public Task<AttributeModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<AttributeModel>(Queries.Get, new {id});
        }

        public Task<int> Create(AttributeModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(AttributeModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}