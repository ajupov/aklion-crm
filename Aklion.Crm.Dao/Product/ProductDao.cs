using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Product;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.Product
{
    public class ProductDao : IProductDao
    {
        private readonly IDao _dao;

        public ProductDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ProductModel>>> GetPagedListAsync(ProductParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductModel, ProductParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(ProductAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync(parameter);
        }

        public Task<ProductModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductModel>(id);
        }

        public Task<int> CreateAsync(ProductModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductModel>(id);
        }
    }
}