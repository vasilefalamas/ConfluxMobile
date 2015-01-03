using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conflux.Core.Models
{
    public interface IIncrementalSource<T>
    {
        Task<IEnumerable<T>> GetPagedItems(uint offset, uint limit = 10);
    }
}
