﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderAttribute;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.OrderAttribute
{
    public class OrderAttributeDao : IOrderAttributeDao
    {
        private readonly IDao _dao;

        public OrderAttributeDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<OrderAttributeModel>>> GetPagedListAsync(OrderAttributeParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderAttributeModel, OrderAttributeParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(OrderAttributeAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync(parameter);
        }

        public Task<OrderAttributeModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderAttributeModel>(id);
        }

        public Task<int> CreateAsync(OrderAttributeModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderAttributeModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderAttributeModel>(id);
        }
    }
}