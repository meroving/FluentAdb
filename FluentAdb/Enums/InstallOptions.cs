using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum InstallOptions
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Install the package with forward lock.
        /// </summary>
        WithForwardLock = 0x1,

        /// <summary>
        /// Reinstall an exisiting app, keeping its data.
        /// </summary>
        ReinstallKeepingData = 0x2,

        /// <summary>
        /// Install package on the shared mass storage (such as sdcard).
        /// </summary>
        InstallToShared = 0x4,

        /// <summary>
        /// Install package on the internal system memory.
        /// </summary>
        InstallToInternal = 0x8,
    }

    public static class InstallOptionsEx
    {
        public static string GenerateString(this InstallOptions options)
        {
            string optionsString = string.Empty;
            if ((options & InstallOptions.WithForwardLock) != 0)
                optionsString += " -l";
            if ((options & InstallOptions.ReinstallKeepingData) != 0)
                optionsString += " -r";
            if ((options & InstallOptions.InstallToShared) != 0)
                optionsString += " -s";
            if ((options & InstallOptions.InstallToInternal) != 0)
                optionsString += " -f";

            return optionsString;
        }
    }
}
