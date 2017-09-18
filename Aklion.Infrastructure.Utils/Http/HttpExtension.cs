using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Aklion.Infrastructure.Utils.Json;

namespace Aklion.Infrastructure.Utils.Http
{
    public static class HttpExtension
    {
        private const int DefaultSize = 10;
        private const string Id = "id";
        private const string MediaType = "application/json";
        private const string AmpersandMark = "&";
        private const string QuestionMark = "?";
        private const string EquallyMark = "=";
        private const string SlashMark = "/";

        public static string ToQueryParams(this object parameters)
        {
            var result = TypeDescriptor.GetProperties(parameters)
                .Cast<PropertyDescriptor>()
                .Where(p => p.Name != Id)
                .Select(p => $"{p.Name}{EquallyMark}{p.GetValue(parameters)}")
                .ToList();

            return result.Any()
                ? $"{QuestionMark}{string.Join(AmpersandMark, result)}"
                : string.Empty;
        }

        public static string ToId(this object parameters)
        {
            var result = TypeDescriptor.GetProperties(parameters)
                .Cast<PropertyDescriptor>()
                .FirstOrDefault(p => p.Name == Id);

            return result != null
                ? $"{SlashMark}{result.GetValue(parameters)}"
                : string.Empty;
        }

        public static StringContent ToStringContent(this object model)
        {
            return new StringContent(model.ToJsonString(), Encoding.UTF8, MediaType);
        }

        public static int ToDataBasePage(this int page)
        {
            return page > 0 ? page - 1 : 0;
        }

        public static int ToDataBaseSize(this int size)
        {
            return size > 0 ? size : DefaultSize;
        }
    }
}