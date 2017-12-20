using System.Collections.Generic;

namespace Aklion.Infrastructure.Mapper
{
    public static class Mapper
    {
        public static TNewModel MapNew<TNewModel>(this object model) where TNewModel : new()
        {
            return new TNewModel();
        }

        public static List<TNewModel> MapListNew<TNewModel>(this IEnumerable<object> model) where TNewModel : new()
        {
            return new List<TNewModel>();
        }

        public static TModel MapFrom<TModel>(this TModel model, object from)
        {
            return model;
        }
    }
}