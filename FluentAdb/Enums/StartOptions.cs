using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum StartOptions
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Enable debugging
        /// </summary>
        EnableDebugging = 0x1, //-D

        /// <summary>
        /// Wait for launch to complete
        /// </summary>
        WaitForLaunchComplete = 0x2, //-W

        /// <summary>
        /// Force stop the target app before starting the activity
        /// </summary>
        ForceStop = 0x4 //-S
    }
}
