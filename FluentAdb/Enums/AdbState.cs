using System;

namespace FluentAdb.Enums
{
    /// <summary>
    /// The connection state of the device/emulator instane
    /// </summary>
    public enum AdbState
    {   
        /// <summary>
        /// The instance state is unknown (unknown output from adb)
        /// </summary>
        Unknown,

        /// <summary>
        /// The instance is not connected to adb or is not responding
        /// </summary>
        Offline,

        [Obsolete]
        Bootloader,

        /// <summary>
        /// The instance is now connected to the adb server
        /// </summary>
        Device
    }
}
