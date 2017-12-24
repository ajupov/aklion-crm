using System.Collections.Generic;

namespace Aklion.Infrastructure.Mapper
{
    public static class Mapper
    {
        public static TNewModel MapNew<TNewModel>(this object model) where TNewModel : new()
        {
            return AutoMapper.Mapper.Map<TNewModel>(model);
        }

        public static List<TNewModel> MapListNew<TNewModel>(this IEnumerable<object> models) where TNewModel : new()
        {
            return AutoMapper.Mapper.Map<List<TNewModel>>(models);
        }

        public static TModel MapFrom<TModel>(this TModel model, object from)
        {
            return AutoMapper.Mapper.Map(from, model);
        }
    }
}