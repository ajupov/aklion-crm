﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ClientAttributeLink;
using Infrastructure.Dao;

namespace Crm.Dao.ClientAttributeLink
{
    public class ClientAttributeLinkDao : IClientAttributeLinkDao
    {
        private readonly IDao _dao;

        public ClientAttributeLinkDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<ClientAttributeLinkModel> List)> GetPagedListAsync(ClientAttributeLinkParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ClientAttributeLinkModel, ClientAttributeLinkParameterModel>(parameter);
        }

        public Task<ClientAttributeLinkModel> GetAsync(int id)
        {
            return _dao.GetAsync<ClientAttributeLinkModel>(id);
        }

        public Task<int> CreateAsync(ClientAttributeLinkModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ClientAttributeLinkModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ClientAttributeLinkModel>(id);
        }
    }
}