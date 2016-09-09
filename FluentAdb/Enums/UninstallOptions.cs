using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum UninstallOptions
    {
        None = 0x0,
        KeepData = 0x1,
    }

    public static class UninstallOptionsEx
    {
        public static string GenerateString(this UninstallOptions options)
        {
            string optionsString = string.Empty;
            if ((options & UninstallOptions.KeepData) != 0)
                optionsString += " -k";

            return optionsString;
        }
    }
}
