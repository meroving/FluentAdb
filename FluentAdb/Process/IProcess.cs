using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FluentAdb.Process
{
    public interface IProcess
    {
        Task<IProcessResult> RunAsync();
        Task<IProcessResult> RunAsync(CancellationToken cancellationToken);

        IObservable<string> Output { get; }

        IProcess WithOutputCache();
        int ExitCode { get; }
        bool HasExited { get; }
        ProcessStartInfo StartInfo { get; }
        bool Start();
        void Kill();
        string Name { get; }
    }
}