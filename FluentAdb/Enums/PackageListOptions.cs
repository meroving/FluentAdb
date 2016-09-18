using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum PackageListOptions
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Get packages associated file
        /// </summary>
        AssociatedFile = 0x1,

        /// <summary>
        /// Filter to only show disabled packages
        /// </summary>
        OnlyDisabled = 0x2,

        /// <summary>
        /// Filter to only show enabled packages.
        /// </summary>
        OnlyEnabled = 0x4,

        /// <summary>
        /// Filter to only show system packages.
        /// </summary>
        OnlySystem = 0x8,

        /// <summary>
        /// Filter to only show third party packages.
        /// </summary>
        OnlyThirdparty = 0x10,

        /// <summary>
        /// Get the installer for the packages.
        /// </summary>
        Installer = 0x20,

        /// <summary>
        /// Also include uninstalled packages.
        /// </summary>
        Uninstalled = 0x40
    }

    public static class PackageListOptionsEx
    {
        public static string GenerateString(this PackageListOptions options)
        {
            string optionsString = string.Empty;
            if ((options & PackageListOptions.AssociatedFile) != 0)
                optionsString += " -f";
            if ((options & PackageListOptions.OnlyDisabled) != 0)
                optionsString += " -d";
            if ((options & PackageListOptions.OnlyEnabled) != 0)
                optionsString += " -e";
            if ((options & PackageListOptions.OnlySystem) != 0)
                optionsString += " -s";
            if ((options & PackageListOptions.OnlyThirdparty) != 0)
                optionsString += " -3";
            if ((options & PackageListOptions.Installer) != 0)
                optionsString += " -i";
            if ((options & PackageListOptions.Uninstalled) != 0)
                optionsString += " -u";

            return optionsString;
        }
    }
}
