using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Infrastructure.DataBaseExecutor.Reader
{
    public interface IReader
    {
        Task<T> SelectOneAsync<T>();

        Task<List<T>> SelectListAsync<T>();
    }
}