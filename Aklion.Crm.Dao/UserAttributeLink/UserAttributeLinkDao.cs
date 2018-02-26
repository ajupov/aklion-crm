using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserAttributeLink;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.UserAttributeLink
{
    public class UserAttributeLinkDao : IUserAttributeLinkDao
    {
        private readonly IDao _dao;

        public UserAttributeLinkDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<UserAttributeLinkModel> List)> GetPagedListAsync(UserAttributeLinkParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserAttributeLinkModel, UserAttributeLinkParameterModel>(parameter);
        }

        public Task<UserAttributeLinkModel> GetAsync(int id)
        {
            return _dao.GetAsync<UserAttributeLinkModel>(id);
        }

        public Task<int> CreateAsync(UserAttributeLinkModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(UserAttributeLinkModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<UserAttributeLinkModel>(id);
        }
    }
}