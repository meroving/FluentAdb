using System.Threading;
using System.Threading.Tasks;

namespace FluentAdb.Interfaces
{
    public interface IDumpSys
    {
        Task<string> DumpService(string service, CancellationToken cancellationToken = default(CancellationToken));
    }
}
