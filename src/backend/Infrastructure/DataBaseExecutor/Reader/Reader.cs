using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.DataBaseExecutor.Reader
{
    public class Reader : IReader
    {
        private readonly SqlMapper.GridReader _reader;

        public Reader(SqlMapper.GridReader reader)
        {
            _reader = reader;
        }

        public async Task<T> SelectOneAsync<T>()
        {
            return await _reader.ReadFirstOrDefaultAsync<T>().ConfigureAwait(false);
        }

        public async Task<List<T>> SelectListAsync<T>()
        {
            return (await _reader.ReadAsync<T>().ConfigureAwait(false)).ToList();
        }
    }
}