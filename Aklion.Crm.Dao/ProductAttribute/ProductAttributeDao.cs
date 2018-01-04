using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttribute;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.ProductAttribute
{
    public class ProductAttributeDao : IProductAttributeDao
    {
        private readonly IDao _dao;

        public ProductAttributeDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ProductAttributeModel>>> GetPagedListAsync(ProductAttributeParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductAttributeModel, ProductAttributeParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(ProductAttributeAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<ProductAttributeModel, ProductAttributeAutocompleteParameterModel>(parameter);
        }

        public Task<ProductAttributeModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductAttributeModel>(id);
        }

        public Task<int> CreateAsync(ProductAttributeModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductAttributeModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductAttributeModel>(id);
        }
    }
}