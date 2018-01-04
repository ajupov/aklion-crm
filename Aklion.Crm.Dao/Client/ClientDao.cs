using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Client;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.Client
{
    public class ClientDao : IClientDao
    {
        private readonly IDao _dao;

        public ClientDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ClientModel>>> GetPagedListAsync(ClientParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ClientModel, ClientParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(ClientAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<ClientModel, ClientAutocompleteParameterModel>(parameter);
        }

        public Task<ClientModel> GetAsync(int id)
        {
            return _dao.GetAsync<ClientModel>(id);
        }

        public Task<int> CreateAsync(ClientModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ClientModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ClientModel>(id);
        }
    }
}