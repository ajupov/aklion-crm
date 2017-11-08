using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;

namespace Aklion.Crm.Mappers
{
    public static class Mapper
    {
        public static List<AutocompleteModel> Map(this IEnumerable<Domain.AutocompleteModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static AutocompleteModel Map(this Domain.AutocompleteModel model)
        {
            return model == null
                ? null
                : new AutocompleteModel
                {
                    Id = model.Id,
                    Value = model.Value
                };
        }
    }
}