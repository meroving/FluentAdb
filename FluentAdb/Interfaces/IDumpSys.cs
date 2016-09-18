using System.Threading;
using System.Threading.Tasks;

namespace FluentAdb.Interfaces
{
    public interface IDumpSys
    {
        /// <summary>
        /// Dumps data of service to the screen.
        /// </summary>
        /// <param name="service">Service name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> DumpService(string service, CancellationToken cancellationToken = default(CancellationToken));
    }
}
