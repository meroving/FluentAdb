using System;
using System.Diagnostics;

namespace FluentAdb
{
    public class NonZeroExitCodeException : Exception
    {
        public int ExitCode { get; private set; }
        public string Output { get; private set; }

        public NonZeroExitCodeException(int exitCode, ProcessStartInfo processStartInfo, string output)
            : base(
                string.Format("Process returned non-zero exit code. Executed command was: {0} {1}",
                    processStartInfo.FileName, processStartInfo.Arguments))
        {
            Output = output;
            ExitCode = exitCode;
        }
    }
}
