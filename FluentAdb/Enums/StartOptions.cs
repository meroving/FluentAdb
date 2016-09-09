using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum StartOptions
    {
        None = 0x0,
        EnableDebugging = 0x1, //-D
        WaitForLaunchComplete = 0x2, //-W
        ForceStop = 0x4 //-S
    }
}
