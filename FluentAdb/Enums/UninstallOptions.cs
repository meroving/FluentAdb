using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum UninstallOptions
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Keep the data and cache directories around after package removal.
        /// </summary>
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
