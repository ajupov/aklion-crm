using System.Threading.Tasks;

namespace Infrastructure.ApiClient
{
    public interface IApiClient
    {
        Task<TResponseModel> GetAsync<TResponseModel>(string url, object parameters = null);

        Task PostAsync<TRequestModel>(string url, TRequestModel model);

        Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url, TRequestModel model);

        Task PutAsync<TRequestModel>(string url, TRequestModel model);

        Task PatchAsync<TRequestModel>(string url, TRequestModel model);

        Task DeleteAsync(string url, object parameters);
    }
}