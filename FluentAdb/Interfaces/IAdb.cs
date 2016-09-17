using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IAdb
    {
        /// <summary>
        /// Current adb executable path
        /// </summary>
        string AdbExecutablePath { get; }

        /// <summary>
        /// Direct an adb command to the only attached USB device.
        /// </summary>
        IAdbTargeted SingleDevice { get; }

        /// <summary>
        /// Direct an adb command to the only running emulator instance.
        /// </summary>
        IAdbTargeted SingleEmulator { get; }

        /// <summary>
        /// Direct an adb command a specific emulator/device instance, referred to by its adb-assigned serial number (such as "emulator-5556").
        /// </summary>
        /// <param name="serialNumber">adb-assigned serial number</param>
        /// <returns>Targeted adb instance</returns>
        IAdbTargeted Target(string serialNumber);


        Task<AdbState> GetState(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Prints a list of all attached emulator/device instances.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of devices</returns>
        Task<IEnumerable<IDeviceInfo>> GetDevices(CancellationToken cancellationToken = default(CancellationToken));


        Task<bool> ConnectWiFiDevice(IPAddress ipAdress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get adb version number.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Version number string</returns>
        Task<string> Version(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks whether the adb server process is running and starts it, if not.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task StartServer(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Terminates the adb server process.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopServer(CancellationToken cancellationToken = default(CancellationToken));
    }
}
