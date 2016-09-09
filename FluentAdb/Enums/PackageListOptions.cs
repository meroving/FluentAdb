using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum PackageListOptions
    {
        None = 0x0,
        AssociatedFile = 0x1,
        OnlyDisabled = 0x2,
        OnlyEnabled = 0x4,
        OnlySystem = 0x8,
        OnlyThirdparty = 0x10,
        Installer = 0x20,
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
