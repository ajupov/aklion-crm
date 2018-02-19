using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Aklion.Infrastructure.Json;

namespace Aklion.Infrastructure.Http
{
    public static class HttpExtension
    {
        public static string ToQueryParams(this object parameters)
        {
            var result = TypeDescriptor.GetProperties(parameters)
                .Cast<PropertyDescriptor>()
                .Where(p => p.Name != "id")
                .Select(p => $"{p.Name}={p.GetValue(parameters)}")
                .ToList();

            return result.Any() ? $"?{string.Join("&", result)}" : string.Empty;
        }

        public static string ToId(this object parameters)
        {
            var result = TypeDescriptor.GetProperties(parameters)
                .Cast<PropertyDescriptor>()
                .FirstOrDefault(p => p.Name == "id");

            return result != null ? $"/{result.GetValue(parameters)}" : string.Empty;
        }

        public static StringContent ToStringContent(this object model)
        {
            return new StringContent(model.ToJsonString(), Encoding.UTF8, "application/json");
        }
    }
}