using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IActivityManager
    {
        Task<bool> Start(Intent intent, StartOptions options = StartOptions.None, int repeatCount = -1, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
        Task StartService(Intent intent, InUser? user = null, bool log = true, CancellationToken cancellationToken = default(CancellationToken));
        Task ForceStop(string package, CancellationToken cancellationToken = default(CancellationToken));
        Task Kill(string package, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
        Task KillAll(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Broadcast(Intent intent, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
