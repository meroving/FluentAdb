using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Process;
using Rhino.Mocks;

namespace FluentAdb.Tests.Util
{
    public class TestProcessManager : IProcessManager
    {
        private readonly IProcessManager _processManager;

        public TestProcessManager()
        {
            _processManager = MockRepository.GenerateStub<IProcessManager>();
        }

        public IProcessManager Stub { get { return _processManager; } }

        public void AddProcess(IEnumerable<string> output, int repeat = 0)
        {
            var process = CreateProcess(output);
            var methodOptions = _processManager.Stub(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                    .Return(process);
            if (repeat > 0)
            {
                methodOptions.Repeat.Times(repeat);
            }
        }

        public void AddProcess(IEnumerable<string> output, string commands, int repeat = 0)
        {
            var process = CreateProcess(output);
            var methodOptions = _processManager.Stub(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(c => c.Contains(commands))))
                    .Return(process);
            if (repeat > 0)
            {
                methodOptions.Repeat.Times(repeat);
            }
        }

        private IProcess CreateProcess(IEnumerable<string> output)
        {
            var process = MockRepository.GenerateStub<IProcess>();
            process.Stub(_ => _.RunAsync(Arg<CancellationToken>.Is.Anything))
                .Return(Task.FromResult((IProcessResult)new ProcessResult(process, output)));

            process.Stub(p => p.Output).Return(new Subject<string>());
            process.Stub(p => p.WithOutputCache()).Return(process);
            return process;
        }

        #region IProcessManager members

        public IProcess CreateProcess(ProcessStartInfo processStartInfo)
        {
            throw new NotImplementedException();
        }

        public IProcess CreateProcess(string fileName)
        {
            throw new NotImplementedException();
        }

        public IProcess CreateProcess(string fileName, string arguments)
        {
            return _processManager.CreateProcess(fileName, arguments);
        }

        public IProcess CreateProcess(string fileName, string arguments, bool errorDialog)
        {
            throw new NotImplementedException();
        }

        public IProcess CreateProcess(string fileName, string userName, SecureString password, string domain)
        {
            throw new NotImplementedException();
        }

        public IProcess CreateProcess(string fileName, string arguments, string userName, SecureString password, string domain)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}