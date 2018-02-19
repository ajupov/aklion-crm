using System.Net.Http;
using System.Threading.Tasks;
using Aklion.Infrastructure.Http;
using Aklion.Infrastructure.Json;

namespace Aklion.Infrastructure.ApiClient
{
    public class ApiClient : IApiClient
    {
        public async Task<TResponseModel> GetAsync<TResponseModel>(string url, object parameters = null)
        {
            var fullUrl = GetUrl(url, parameters);

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(fullUrl).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                    return default(TResponseModel);

                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.FromJsonString<TResponseModel>();
            }
        }

        public async Task PostAsync<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PostAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                    return default(TResponseModel);

                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.FromJsonString<TResponseModel>();
            }
        }

        public async Task PutAsync<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PutAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task PatchAsync<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PutAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(string url, object parameters)
        {
            var fullUrl = GetUrl(url, parameters);

            using (var client = new HttpClient())
            {
                await client.DeleteAsync(fullUrl).ConfigureAwait(false);
            }
        }

        private static string GetUrl(string resourceUrl, object parameters = null)
        {
            return $"{resourceUrl}{parameters.ToId()}{parameters.ToQueryParams()}";
        }
    }
}