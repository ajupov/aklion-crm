using System.Threading.Tasks;

namespace Aklion.Infrastructure.ApiClient
{
    public interface IApiClient
    {
        Task<TResponseModel> Get<TResponseModel>(string url, object parameters = null);

        Task Post<TRequestModel>(string url, TRequestModel model);

        Task<TResponseModel> Post<TRequestModel, TResponseModel>(string url, TRequestModel model);

        Task Put<TRequestModel>(string url, TRequestModel model);

        Task Patch<TRequestModel>(string url, TRequestModel model);

        Task Delete(string url, object parameters);
    }
}