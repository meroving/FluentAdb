using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Interfaces;
using FluentAdb.Process;
using FluentAdb.Util;
using System.Net;

namespace FluentAdb
{
    public partial class Adb : IAdb, IAdbTargeted
    {
        private readonly IProcessManager _processManager;

        public static void SetMainAdbPath(string adbPath)
        {
            if (!File.Exists(adbPath))
                throw new ArgumentException("Adb not exists at this path");

            MainAdbPath = adbPath;
        }

        public static string MainAdbPath;
        private readonly string _adbPath;

        public Adb()
        {
            AdbCommandBuilder = new StringBuilder();
            _processManager = new ProcessManager();
        }

        internal Adb(IProcessManager processManager)
        {
            AdbCommandBuilder = new StringBuilder();
            _processManager = processManager;
        }


        public IAdb Clone(string externalAdbPath = null)
        {
            var clonedAdb = new Adb();

            return clonedAdb;
        }

        public string FilePath
        {
            get { return _adbPath ?? MainAdbPath; }
        }

        private Adb(Adb adb, string command, params object[] parameters)
        {
            _adbPath = adb._adbPath;
            _processManager = adb._processManager;

            AdbCommandBuilder = new StringBuilder(adb.AdbCommandBuilder.ToString());
            AdbCommandBuilder.Append(" ");
            AdbCommandBuilder.AppendFormat(command, parameters);
        }

        protected StringBuilder AdbCommandBuilder;

        #region IAdb
        public IAdbTargeted SingleDevice()
        {
            return new Adb(this, "-d");
        }

        public IAdbTargeted SingleEmulator()
        {
            return new Adb(this, "-e");
        }

        public IAdbTargeted Target(string id)
        {
            return new Adb(this, "-s \"{0}\"", id);
        }
        #endregion

        #region IAdbTargeted

        public IObservable<string> Logcat(LogcatOptions options = LogcatOptions.None,
            LogOutputFormat format = LogOutputFormat.Brief,
            CancellationToken cancellationToken = default(CancellationToken), params LogFilter[] filters)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                string formatString = format == LogOutputFormat.Brief ? string.Empty : string.Format("-v {0}", format.ToString().ToLower());
                string filtersString = filters.Aggregate("", (acc, f) => acc + " " + f.ToString());
                var process = new Adb(this, "logcat {0} {1} {2}", options.GenerateString(), formatString, filtersString).CreateProcess(FilePath, cts, 0, false);
                process.RunAsync(cts.Token);
                return process.Output;
            }
        }

        public IShell Shell
        {
            get
            {
                return new Adb(this, "shell");
            }
        }

        public async Task<IEnumerable<IDeviceInfo>> GetDevices(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "devices").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines().ToList();
            if (!lines.Any())
            {
                return new List<IDeviceInfo>();
            }
            lines.RemoveRange(0, 1);
            return lines.Select(l => new AdbDeviceInfo(l));
        }

        public async Task<string> Version(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "version").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> Install(string apkPath, InstallOptions options = InstallOptions.None, CancellationToken cancellationToken = default(CancellationToken))
        {
            int timeout = GetTimeoutByFileSize(apkPath);
            var output = await new Adb(this, "install {0} {1}", options.GenerateString(), apkPath.ExceptionQuoteIfNeeded())
                .RunAsync(timeout, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (!output.Contains("Error:") && !output.Contains("Failure"))
            {
                return InstallationResult.Success;
            }
            else
            {
                try
                {
                    var lines = output.ToLines();
                    string failureLine = lines.FirstOrDefault(l => l.Contains("Failure"));
                    if (failureLine == null)
                        return InstallationResult.InstallParseFailedUnexpectedException;
                    int length = "Failure".Length;
                    failureLine = failureLine.Substring(length, failureLine.Length - length).Trim(' ', '[', ']');

                    return failureLine;

                }
                catch (Exception)
                {
                    return InstallationResult.InstallParseFailedUnexpectedException;
                }
            }
        }

        private int GetTimeoutByFileSize(string apkPath)
        {
            var fileInfo = new FileInfo(apkPath);
            if (fileInfo.Exists)
            {
                long dataSize = new FileInfo(apkPath).Length;
                int m10Size = (int)Math.Ceiling(dataSize / (1024.0 * 1024.0 * 10));

                return m10Size * 60000;
            }
            return 10 * 60000;
        }

        public async Task<string> Pull(string remotePath, string localPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "pull \"{0}\" \"{1}\"", remotePath, localPath).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> GetScreenshot(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "shell screencap -p \"{0}\"", file).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> Push(string localPath, string remotePath, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "push {0} {1}", localPath.QuoteIfNeeded(), remotePath.QuoteIfNeeded()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<AdbState> GetState(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "get-state").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines().ToList();

            if (!lines.Any())
                return AdbState.Unknown;
            if (lines.Contains("device"))
                return AdbState.Device;
            else if (lines.Contains("offline"))
                return AdbState.Offline;
            else if (lines.Contains("bootloader"))
                return AdbState.Bootloader;
            else return AdbState.Unknown;
        }


        public static async Task StartServer(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var process = new Adb(new Adb(), "start-server").CreateProcess(MainAdbPath, cts, 0);
                await process.RunAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task StopServer(IProcessManager processManager, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var process = new Adb(new Adb(), "kill-server").CreateProcess(MainAdbPath, cts, 0);
                await process.RunAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task Backup(BackupOptions options = BackupOptions.None, IEnumerable<string> packages = null, string backupFile = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string fileString = backupFile != null ? string.Format("-f {0}", backupFile.QuoteIfNeeded()) : "";
            string packagesString = "";
            if (packages != null)
            {
                packagesString = packages.Aggregate("", (acc, p) => acc + " " + p);
            }

            await new Adb(this, "backup {0} {1} {2}", fileString, options.GenerateString(), packagesString).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        }

        public async Task Restore(string backupFile, Action<string> outputHandler, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var process = new Adb(this, "restore {0}", backupFile.QuoteIfNeeded()).CreateProcess(FilePath, cts, 0, false);
                IDisposable subscription = null;
                if (outputHandler != null)
                {
                    subscription = process.Output.Subscribe(outputHandler);
                }
                await process.RunAsync(cancellationToken);
                if (subscription != null)
                {
                    subscription.Dispose();
                }
            }
        }

        public async Task<bool> ConnectWiFiDevice(IPAddress ipAdress, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var lines = (await new Adb(this, "connect {0}", ipAdress.ToString()).RunAsync(cancellationToken: cancellationToken)).ToLines().ToList();

                if (!lines.Any() || lines.First().Contains("unable"))
                {
                    return false;
                }
                return true;
            }
            catch (NonZeroExitCodeException)
            {
                return false;
            }
        }
        #endregion

        #region Tools
        private string GetUserString(InUser? user)
        {
            return user.HasValue ? user.ToString() : "";
        }

        private IProcess CreateProcess(string adbPath, CancellationTokenSource cts, int timeout = 1000 * 60 * 10, bool cacheOutput = true)
        {
            string commands = AdbCommandBuilder.ToString().Trim();
            var processManager = _processManager ?? new ProcessManager();
            var process = processManager.CreateProcess(adbPath, commands);
            process.Output.Buffer(TimeSpan.FromSeconds(1)).Subscribe(
                outputChunk =>
                {
                    if (outputChunk.Any(s => s.Contains("error: device not found")))
                        cts.Cancel();
                });

            if (timeout != 0)
            {
                cts.CancelAfter(timeout);
            }
            if (cacheOutput)
            {
                process = process.WithOutputCache();
            }
            return process;
        }

        private async Task<string> RunAsync(int timeout = 1000 * 60 * 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var result = await CreateProcess(FilePath, cts, timeout).RunAsync(cts.Token).ConfigureAwait(false);

                if (result.Process.ExitCode != 0)
                {
                    throw new NonZeroExitCodeException(result.Process.ExitCode, result.Process.StartInfo, result.Output.FromLines());
                }

                return IgnoreDaemonRestarting(result.Output.ToList());
            }
        }

        private static string IgnoreDaemonRestarting(List<string> lines)
        {
            if (lines.Count > 2
                && (lines[0].StartsWith("* daemon not running. starting it now on port")
                    || lines[0].StartsWith("adb server is out of date.  killing..."))
                && lines[1].Equals("* daemon started successfully *"))
            {
                lines.RemoveRange(0, 2);
            }
            return lines.FromLines();
        }
        #endregion

        public static void Die()
        {
            const int triesCount = 3;
            bool needOtherTry = true;
            for (int i = 0; needOtherTry; i++)
            {
                if (i != 0)
                    Thread.Sleep(100);

                needOtherTry = false;
                var exceptions = new List<Exception>();
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("adb");
                foreach (var process in processes)
                {
                    Debug.WriteLine("Try {0}. Killing process {1}, PID: {2}", i.ToString(), process.ProcessName, process.Id.ToString());
                    try
                    {
                        process.Kill();
                    }
                    catch (InvalidOperationException)
                    { }
                    catch (Win32Exception ex)
                    {
                        if (ex.NativeErrorCode == 5)
                        {
                            Debug.WriteLine(
                                "Try {0}. Failed to kill process {1}, PID: {2}; Access denied, process is currently terminating",
                                i.ToString(), process.ProcessName, process.Id.ToString());
                            if (i < triesCount)
                            {
                                needOtherTry = true;
                            }
                            else
                            {
                                exceptions.Add(ex);
                            }
                        }
                        else
                        {
                            exceptions.Add(ex);
                        }
                    }
                }
            }
        }
    }
}
