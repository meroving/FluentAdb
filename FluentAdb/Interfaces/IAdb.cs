using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IAdb
    {
        IAdbTargeted SingleDevice();
        IAdbTargeted SingleEmulator();
        IAdbTargeted Target(string id);
        Task<AdbState> GetState(CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<IDeviceInfo>> GetDevices(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> ConnectWiFiDevice(IPAddress ipAdress, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Version(CancellationToken cancellationToken = default(CancellationToken));
        IAdb Clone(string externalAdbPath = null);
        string FilePath { get; }
    }
}
