using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.Domain.CrmUserContext;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Storage.DataBaseExecutor;

namespace Aklion.Crm.Dao.CrmUserContext
{
    public class CrmUserContextDao : ICrmUserContextDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public CrmUserContextDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<CrmUserContextModel> Get(string login, int selectedStoreId)
        {
            return _dataBaseExecutor.SelectMultiple(Queries.Get, async r =>
            {
                var crmUserContextModel = await r.SelectOne<CrmUserContextModel>().ConfigureAwait(false);
                crmUserContextModel.Permissions = await r.SelectList<Permission>().ConfigureAwait(false);
                crmUserContextModel.AvialableStores =
                    (await r.SelectList<KeyValuePair<int, string>>().ConfigureAwait(false)).ToDictionary(k => k.Key,
                        v => v.Value);

                return crmUserContextModel;
            }, new {login, selectedStoreId});
        }
    }
}