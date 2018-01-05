using System.Threading.Tasks;

namespace Aklion.Crm.Business.Store
{
    public interface IStoreService
    {
        Task<string> GenerateApiSecretAsync(int userId, int storeId, int id);
    }
}