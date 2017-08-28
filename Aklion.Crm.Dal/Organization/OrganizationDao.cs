using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Organization;
using Aklion.InfrastructureV1.DataBaseExecutor;

namespace Aklion.Crm.DateAccessLayer.Organization
{
    public class OrganizationDao : IOrganizationDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public OrganizationDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<OrganizationDomainModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<OrganizationDomainModel>(Queries.Select, new {id});
        }

        public Task<List<OrganizationDomainModel>> GetList()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<OrganizationDomainModel>> GetPagedList(int page, int size)
        {
            throw new System.NotImplementedException();
        }

        public Task Create(OrganizationDomainModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(OrganizationDomainModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}