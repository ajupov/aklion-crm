using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttributeLink;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.ProductAttributeLink
{
    public class ProductAttributeLinkDao : IProductAttributeLinkDao
    {
        private readonly IDao _dao;

        public ProductAttributeLinkDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ProductAttributeLinkModel>>> GetPagedListAsync(ProductAttributeLinkParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductAttributeLinkModel, ProductAttributeLinkParameterModel>(parameter);
        }

        public Task<ProductAttributeLinkModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductAttributeLinkModel>(id);
        }

        public Task<int> CreateAsync(ProductAttributeLinkModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductAttributeLinkModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductAttributeLinkModel>(id);
        }
    }
}