using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Aklion.Infrastructure.Storage.DataBaseExecutor.Readers
{
    public class Reader : IReader
    {
        private readonly SqlMapper.GridReader _reader;

        public Reader(SqlMapper.GridReader reader)
        {
            _reader = reader;
        }

        public async Task<T> SelectOne<T>()
        {
            return await _reader.ReadFirstAsync<T>().ConfigureAwait(false);
        }

        public async Task<List<T>> SelectList<T>()
        {
            return (await _reader.ReadAsync<T>().ConfigureAwait(false)).ToList();
        }
    }
}