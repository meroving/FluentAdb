using System.Diagnostics;
using System.Security;

namespace FluentAdb.Process
{
    public interface IProcessManager
    {
        IProcess CreateProcess(ProcessStartInfo processStartInfo);
        IProcess CreateProcess(string fileName);
        IProcess CreateProcess(string fileName, string arguments);
        IProcess CreateProcess(string fileName, string arguments, bool errorDialog);
        IProcess CreateProcess(string fileName, string userName, SecureString password, string domain);
        IProcess CreateProcess(string fileName, string arguments, string userName, SecureString password, string domain);
    }
}