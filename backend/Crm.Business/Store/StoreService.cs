using System.Threading.Tasks;
using Crm.Dao.Store;
using Infrastructure.Random;

namespace Crm.Business.Store
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