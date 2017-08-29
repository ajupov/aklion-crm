using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.InfrastructureV1.DataBaseExecutor;

namespace Aklion.Crm.Dao.Organization
{
    public class OrganizationDao : IOrganizationDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public OrganizationDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Models.Organization> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<Models.Organization>(Queries.Get, new {id});
        }

        public Task<List<Models.Organization>> GetList(int page, int size)
        {
            return _dataBaseExecutor.SelectList<Models.Organization>(Queries.GetList, new {page, size});
        }

        public Task<int> Create(Models.Organization model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(Models.Organization model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}