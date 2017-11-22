using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor.Readers
{
    public interface IReader
    {
        Task<T> SelectOne<T>();

        Task<List<T>> SelectList<T>();
    }
}