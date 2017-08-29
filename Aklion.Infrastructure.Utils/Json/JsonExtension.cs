using Newtonsoft.Json;

namespace Aklion.Infrastructure.Utils.Json
{
    public static class JsonExtension
    {
        public static TModel FromJsonString<TModel>(this string str)
        {
            return JsonConvert.DeserializeObject<TModel>(str);
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}