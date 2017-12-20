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

        public async Task<string> GenerateApiSecretAsync(int id)
        {
            var store = await _storeDao.GetAsync(id).ConfigureAwait(false);

            store.ApiSecret = RandomGenerator.GenerateAlphaNumbericString(16);

            await _storeDao.UpdateAsync(store).ConfigureAwait(false);

            return store.ApiSecret;
        }
    }
}