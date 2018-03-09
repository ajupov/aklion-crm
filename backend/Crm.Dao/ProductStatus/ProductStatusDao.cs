using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ProductStatus;
using Infrastructure.Dao;

namespace Crm.Dao.ProductStatus
{
    public class ProductStatusDao : IProductStatusDao
    {
        private readonly IDao _dao;

        public ProductStatusDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<ProductStatusModel> List)> GetPagedListAsync(ProductStatusParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductStatusModel, ProductStatusParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetSelectAsync(ProductStatusSelectParameterModel parameter)
        {
            return _dao.GetForSelectAsync<ProductStatusModel, ProductStatusSelectParameterModel>(parameter);
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