using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentAdb.Process
{
    public class OsProcess : IProcess
    {
        private readonly System.Diagnostics.Process _process;
        private readonly List<string> _output;

        private readonly object _lock = new object();

        private IObservable<string> _received;
        private IObservable<object> _exited;

        public OsProcess(ProcessStartInfo processStartInfo)
        {
            _process = new System.Diagnostics.Process { StartInfo = processStartInfo, EnableRaisingEvents = true };
            _output = new List<string>();

            _exited = Observable.FromEventPattern(
                    h => _process.Exited += h,
                    h => _process.Exited -= h);

            _received = Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>(
                h =>
                {
                    _process.OutputDataReceived += h;
                    _process.ErrorDataReceived += h;
                },
                h =>
                {
                    _process.OutputDataReceived -= h;
                    _process.ErrorDataReceived -= h;
                })
                .TakeUntil(_exited)
                .Where(e => !string.IsNullOrEmpty(e.EventArgs.Data))
                .Select(e => e.EventArgs.Data);
        }

        public IProcess WithOutputCache()
        {
            _received.Subscribe(s => _output.Add(s));
            return this;
        }

        public Task<IProcessResult> RunAsync()
        {
            return RunAsync(CancellationToken.None);
        }

        public Task<IProcessResult> RunAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<IProcessResult>();

            var tokenRegistration = cancellationToken.Register(() =>
            {
                tcs.TrySetCanceled();
                Kill();
            });

            _exited.Subscribe(o =>
            {
                tokenRegistration.Dispose();
                tcs.TrySetResult(new ProcessResult(this, _output));
            });

            cancellationToken.ThrowIfCancellationRequested();

            if (Start() == false)
            {
                tcs.TrySetException(new InvalidOperationException("Failed to start process"));
            }
            return tcs.Task;
        }

        public IObservable<string> Output
        {
            get
            {
                return _received;
            }
        }

        public int ExitCode
        {
            get { return _process.ExitCode; }
        }

        public bool HasExited
        {
            get { return _process.HasExited; }
        }

        public string Name
        {
            get { return _process.ProcessName; }
        }

        public ProcessStartInfo StartInfo
        {
            get { return _process.StartInfo; }
        }

        public virtual bool Start()
        {
            var status = _process.Start();
            if (_process.StartInfo.RedirectStandardOutput)
            {
                _process.BeginOutputReadLine();
            }
            if (_process.StartInfo.RedirectStandardError)
            {
                _process.BeginErrorReadLine();
            }
            return status;
        }

        public virtual void Kill()
        {
            try
            {
                _process.Kill();
            }
            catch (InvalidOperationException)
            {
                Debug.WriteLine("Process was already stopped");
            }
            catch (Exception ex)
            {
            }

        }
    }
}
