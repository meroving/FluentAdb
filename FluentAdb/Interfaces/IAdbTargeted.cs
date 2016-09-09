using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IAdbTargeted
    {
        IShell Shell { get; }

        Task<string> Install(string apkPath, InstallOptions options = InstallOptions.None, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Pull(string remotePath, string localPath, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Push(string localPath, string remotePath, CancellationToken cancellationToken = default(CancellationToken));
        Task Backup(BackupOptions options = BackupOptions.None, IEnumerable<string> packages = null, string backupFile = null, CancellationToken cancellationToken = default(CancellationToken));
        Task Restore(string backupFile, Action<string> outputHandler, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> RunCommand(string command, CancellationToken cancellationToken = default(CancellationToken));
        Task<AdbState> GetState(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetScreenshot(string fileToSave, CancellationToken cancellationToken = default(CancellationToken));
        IObservable<string> Logcat(LogcatOptions options = LogcatOptions.None, LogOutputFormat format = LogOutputFormat.Brief, CancellationToken cancellationToken = default(CancellationToken), params LogFilter[] filters);
    }
}
