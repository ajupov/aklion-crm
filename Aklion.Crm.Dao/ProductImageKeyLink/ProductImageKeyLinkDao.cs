using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductImageKeyLink;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.ProductImageKeyLink
{
    public class ProductImageKeyLinkDao : IProductImageKeyLinkDao
    {
        private readonly IDao _dao;

        public ProductImageKeyLinkDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<ProductImageKeyLinkModel>>> GetPagedListAsync(ProductImageKeyLinkParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ProductImageKeyLinkModel, ProductImageKeyLinkParameterModel>(parameter);
        }

        public Task<ProductImageKeyLinkModel> GetAsync(int id)
        {
            return _dao.GetAsync<ProductImageKeyLinkModel>(id);
        }

        public Task<int> CreateAsync(ProductImageKeyLinkModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ProductImageKeyLinkModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task SetImageAsync(int id, Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductImageKeyLinkModel>(id);
        }
    }
}