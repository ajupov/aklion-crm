using System.Net.Http;
using System.Threading.Tasks;
using Aklion.Infrastructure.Utils.Http;
using Aklion.Infrastructure.Utils.Json;
using Microsoft.Extensions.Configuration;

namespace Aklion.Infrastructure.ApiClient
{
    public class ApiClient : IApiClient
    {
        private const string ApiPrefix = "api";
        
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public ApiClient(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
            _apiVersion = configuration["ApiVersion"];
        }

        public async Task<TResponseModel> Get<TResponseModel>(string url, object parameters = null)
        {
            var fullUrl = GetUrl(url, parameters);

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(fullUrl).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    return default(TResponseModel);
                }

                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.FromJsonString<TResponseModel>();
            }
        }

        public async Task Post<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PostAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task<TResponseModel> Post<TRequestModel, TResponseModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    return default(TResponseModel);
                }

                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.FromJsonString<TResponseModel>();
            }
        }

        public async Task Put<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PutAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task Patch<TRequestModel>(string url, TRequestModel model)
        {
            var fullUrl = GetUrl(url);

            using (var client = new HttpClient())
            {
                await client.PutAsync(fullUrl, model.ToStringContent()).ConfigureAwait(false);
            }
        }

        public async Task Delete(string url, object parameters)
        {
            var fullUrl = GetUrl(url, parameters);

            using (var client = new HttpClient())
            {
                await client.DeleteAsync(fullUrl).ConfigureAwait(false);
            }
        }

        private string GetUrl(string resourceUrl, object parameters = null)
        {
            return $"{_apiUrl}/{ApiPrefix}/{_apiVersion}/{resourceUrl}{parameters.ToId()}{parameters.ToQueryParams()}";
        }
    }
}