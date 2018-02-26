using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductImageKeyLink;
using Aklion.Infrastructure.Dao;
using Aklion.Infrastructure.DataBaseExecutor;
using Dapper;

namespace Aklion.Crm.Dao.ProductImageKeyLink
{
    public class ProductImageKeyLinkDao : IProductImageKeyLinkDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;
        private readonly IDao _dao;

        public ProductImageKeyLinkDao(IDataBaseExecutor dataBaseExecutor, IDao dao)
        {
            _dao = dao;
            _dataBaseExecutor = dataBaseExecutor;
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

            return _dataBaseExecutor.ExecuteAsync(Queries.SetImage, @params);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ProductImageKeyLinkModel>(id);
        }
    }
}