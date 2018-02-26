using System.Threading.Tasks;
using Aklion.Crm.Dao.Store;
using Aklion.Infrastructure.Random;

namespace Aklion.Crm.Business.Store
{
    public class StoreService : IStoreService
    {
        private readonly IStoreDao _storeDao;

        public StoreService(IStoreDao storeDao)
        {
            _storeDao = storeDao;
        }

        public async Task<string> GenerateApiSecretAsync(int userId, int storeId, int id)
        {
            var model = await _storeDao.GetAsync(id).ConfigureAwait(false);

            model.ApiSecret = RandomGenerator.GenerateAlphaNumbericString(16);

            await _storeDao.UpdateAsync(model).ConfigureAwait(false);

            return model.ApiSecret;
        }
    }
}