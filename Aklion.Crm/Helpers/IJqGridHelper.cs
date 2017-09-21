using System.Threading.Tasks;
using Aklion.Crm.Models.JqGrid;

namespace Aklion.Crm.Helpers
{
    public interface IJqGridHelper
    {
        Task<JqGridDataModel> GetData<TModel>(JqGridGetModel model);

        Task Edit<TModel>(JqGridEditModel model) where TModel : new();
    }
}