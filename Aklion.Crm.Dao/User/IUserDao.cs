using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.User;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.User
{
    public interface IUserDao
    {
        Task<Paging<UserModel>> GetPagedList(UserParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByLoginPattern(string pattern);

        Task<UserModel> Get(int id);

        Task<int> Create(UserModel model);

        Task Update(UserModel model);

        Task Delete(int id);
    }
}