using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum InstallOptions
    {
        None = 0x0,
        WithForwardLock = 0x1,
        ReinstallKeepingData = 0x2,
        InstallToShared = 0x4,
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
