using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ClientAttribute;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.ClientAttribute
{
    public class ClientAttributeDao : IClientAttributeDao
    {
        private readonly IDao _dao;

        public ClientAttributeDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ClientAttributeModel>>> GetPagedListAsync(ClientAttributeParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ClientAttributeModel, ClientAttributeParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(ClientAttributeAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<ClientAttributeModel, ClientAttributeAutocompleteParameterModel>(parameter);
        }

        public Task<ClientAttributeModel> GetAsync(int id)
        {
            return _dao.GetAsync<ClientAttributeModel>(id);
        }

        public Task<int> CreateAsync(ClientAttributeModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ClientAttributeModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ClientAttributeModel>(id);
        }
    }
}