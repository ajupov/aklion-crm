using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductStatus;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.ProductStatus
{
    public class ProductStatusDao : IProductStatusDao
    {
        private readonly IDao _dao;

        public ProductStatusDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Dictionary<string, int>> GetForSelectAsync(ProductStatusSelectParameterModel parameter)
        {
            return _dao.GetForSelectAsync<ProductStatusModel, ProductStatusSelectParameterModel>(parameter);
        }

        public Task<Tuple<int, List<ProductStatusModel>>> GetPagedListAsync(ProductStatusParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductStatusModel, ProductStatusParameterModel>(parameter);
        }

        public Task<ProductStatusModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductStatusModel>(id);
        }

        public Task<int> CreateAsync(ProductStatusModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductStatusModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductStatusModel>(id);
        }
    }
}