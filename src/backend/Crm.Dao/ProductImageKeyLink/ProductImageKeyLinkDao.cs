using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Crm.Domain.ProductImageKeyLink;
using Infrastructure.Dao;
using Infrastructure.DataBaseExecutor;
using Dapper;

namespace Crm.Dao.ProductImageKeyLink
{
    public class ProductImageKeyLinkDao : IProductImageKeyLinkDao
    {
        private readonly IDataBaseExecutor _executor;
        private readonly IDao _dao;

        public ProductImageKeyLinkDao(IDataBaseExecutor executor, IDao dao)
        {
            _dao = dao;
            _executor = executor;
        }

        public Task<(int TotalCount, List<ProductImageKeyLinkModel> List)> GetPagedListAsync(ProductImageKeyLinkParameterModel parameter)
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
            var @params = new DynamicParameters();
            @params.Add("@stream", stream, DbType.Binary);

            return _executor.ExecuteAsync(Queries.SetImage, @params);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductImageKeyLinkModel>(id);
        }
    }
}