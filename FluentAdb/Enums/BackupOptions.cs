using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum BackupOptions
    {
        None = 0x0,
        Apk = 0x1,
        NoApk = 0x2,
        Obb = 0x4,
        NoObb = 0x8,
        Shared = 0x10,
        NoShared = 0x20,
        All = 0x40,
        System = 0x80,
        NoSystem = 0x100
    }

    public static class BackupOptionsEx
    {
        public static string GenerateString(this BackupOptions options)
        {
            string optionsString = string.Empty;
            if ((options & BackupOptions.Apk) != 0)
                optionsString += " -apk";
            if ((options & BackupOptions.NoApk) != 0)
                optionsString += " -noapk";
            if ((options & BackupOptions.Obb) != 0)
                optionsString += " -obb";
            if ((options & BackupOptions.NoObb) != 0)
                optionsString += " -noobb";
            if ((options & BackupOptions.Shared) != 0)
                optionsString += " -shared";
            if ((options & BackupOptions.NoShared) != 0)
                optionsString += " -noshared";
            if ((options & BackupOptions.All) != 0)
                optionsString += " -all";
            if ((options & BackupOptions.System) != 0)
                optionsString += " -system";
            if ((options & BackupOptions.NoSystem) != 0)
                optionsString += " -nosystem";

            return optionsString;
        }
    }
}
