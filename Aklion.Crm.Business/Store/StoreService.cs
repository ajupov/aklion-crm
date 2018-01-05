using System.Threading.Tasks;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.Store;
using Aklion.Infrastructure.Random;

namespace Aklion.Crm.Business.Store
{
    public class StoreService : IStoreService
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IStoreDao _storeDao;

        public StoreService(
            IAuditLogService auditLogService,
            IStoreDao storeDao)
        {
            _auditLogService = auditLogService;
            _storeDao = storeDao;
        }

        public async Task<string> GenerateApiSecretAsync(int userId, int storeId, int id)
        {
            var model = await _storeDao.GetAsync(id).ConfigureAwait(false);
            var oldModelClone = model.Clone();

            model.ApiSecret = RandomGenerator.GenerateAlphaNumbericString(16);

            await _storeDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(userId, storeId, oldModelClone, model);

            return model.ApiSecret;
        }
    }
}