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

        /// <summary>
        /// Allow version code downgrade.
        /// </summary>
        AllowDowngrade = 0x10,

        /// <summary>
        /// Grant all permissions listed in the app manifest.
        /// </summary>
        GrantAllPermissions = 0x20,

        /// <summary>
        /// Allow test APKs to be installed.
        /// </summary>
        AllowTestAPKs = 0x40,

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
            if ((options & InstallOptions.AllowDowngrade) != 0)
                optionsString += " -d";
            if ((options & InstallOptions.GrantAllPermissions) != 0)
                optionsString += " -g";
            if ((options & InstallOptions.AllowTestAPKs) != 0)
                optionsString += " -t";

            return optionsString;
        }
    }
}
