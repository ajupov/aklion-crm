using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ProductImageKey;
using Infrastructure.Dao;

namespace Crm.Dao.ProductImageKey
{
    public class ProductImageKeyDao : IProductImageKeyDao
    {
        private readonly IDao _dao;

        public ProductImageKeyDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<ProductImageKeyModel> List)> GetPagedListAsync(ProductImageKeyParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductImageKeyModel, ProductImageKeyParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetSelectAsync(ProductImageKeySelectParameterModel parameter)
        {
            return _dao.GetForSelectAsync<ProductImageKeyModel, ProductImageKeySelectParameterModel>(parameter);
        }

        public Task<ProductImageKeyModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductImageKeyModel>(id);
        }

        public Task<int> CreateAsync(ProductImageKeyModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductImageKeyModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductImageKeyModel>(id);
        }
    }
}