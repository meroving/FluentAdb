using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IAdbTargeted
    {
        /// <summary>
        /// Run a shell command in the target emulator/device instance and then exits the remote shell
        /// </summary>
        IShell Shell { get; }

        /// <summary>
        /// Pushes an Android application (specified as a full path to an .apk file) to an emulator/device.
        /// </summary>
        /// <param name="apkPath">Full path to an .apk file</param>
        /// <param name="options">Installtion options</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Installation result string</returns>
        Task<string> Install(string apkPath, InstallOptions options = InstallOptions.None, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Copies a specified file from an emulator/device instance to computer.
        /// </summary>
        /// <param name="remotePath">Path of file on device</param>
        /// <param name="localPath">Path on computer to pull file</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IOResult> Pull(string remotePath, string localPath, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Copies a specified file from computer to an emulator/device instance.
        /// </summary>
        /// <param name="localPath">Path of file to push</param>
        /// <param name="remotePath">Path on device to push file</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task<IOResult> Push(string localPath, string remotePath, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="packages"></param>
        /// <param name="backupFile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Backup(BackupOptions options = BackupOptions.None, IEnumerable<string> packages = null, string backupFile = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupFile"></param>
        /// <param name="outputHandler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Restore(string backupFile, Action<string> outputHandler, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Gets the adb state of an emulator/device instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Adb state</returns>
        Task<AdbState> GetState(CancellationToken cancellationToken = default(CancellationToken));


        Task<string> GetScreenshot(string fileToSave, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Observe logcat from device
        /// </summary>
        /// <param name="options">Logcat options</param>
        /// <param name="format">Log output format</param>
        /// <param name="cancellationToken"></param>
        /// <param name="filters">Array of logcat filters</param>
        /// <returns>Logcat observable</returns>
        IObservable<string> Logcat(LogcatOptions options = LogcatOptions.None, LogOutputFormat format = LogOutputFormat.Brief, CancellationToken cancellationToken = default(CancellationToken), params LogFilter[] filters);

        /// <summary>
        /// Run arbitrary command to targeted device
        /// </summary>
        /// <param name="command">Command to run</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Adb output</returns>
        Task<string> RunCommand(string command, CancellationToken cancellationToken = default(CancellationToken));
    }
}
