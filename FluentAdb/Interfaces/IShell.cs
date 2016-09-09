using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IShell
    {
        IActivityManager ActivityManager { get; }
        IPackageManager PackageManager { get; }
        IDumpSys DumpSys { get; }
        Task<string> GetProperty(string property, CancellationToken cancellationToken = default(CancellationToken));
        Task<Dictionary<string, string>> GetAllProperties(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> DiskStats(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> RunCommand(string command, CancellationToken cancellationToken = default(CancellationToken));
        Task Input(string text, CancellationToken cancellationToken = default(CancellationToken));
        Task Input(KeyCode key, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> ListDirectory(string directory, CancellationToken cancellationToken = default(CancellationToken));
        Task MakeDirectory(string directory, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Monkey(string package, int eventsCount = 1, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> NetworkConfiguration(CancellationToken cancellationToken = default(CancellationToken));
    }
}
