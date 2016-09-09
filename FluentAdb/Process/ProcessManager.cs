using System.Diagnostics;
using System.Security;

namespace FluentAdb.Process
{
    public class ProcessManager : IProcessManager
    {
        public IProcess CreateProcess(ProcessStartInfo processStartInfo)
        {
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            return new OsProcess(processStartInfo);
        }

        public IProcess CreateProcess(string fileName)
        {
            return CreateProcess(new ProcessStartInfo(fileName));
        }

        public IProcess CreateProcess(string fileName, string arguments)
        {
            return CreateProcess(new ProcessStartInfo(fileName, arguments));
        }

        public IProcess CreateProcess(string fileName, string arguments, bool errorDialog)
        {
            return CreateProcess(new ProcessStartInfo(fileName, arguments)
            {
                ErrorDialog = errorDialog
            });
        }

        public IProcess CreateProcess(string fileName, string userName, SecureString password, string domain)
        {
            return CreateProcess(new ProcessStartInfo(fileName)
            {
                UserName = userName,
                Password = password,
                Domain = domain
            });
        }

        public IProcess CreateProcess(string fileName, string arguments, string userName, SecureString password, string domain)
        {
            return CreateProcess(new ProcessStartInfo(fileName, arguments)
            {
                UserName = userName,
                Password = password,
                Domain = domain
            });
        }
    }
}