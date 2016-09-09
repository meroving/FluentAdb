using System;

namespace FluentAdb.Enums
{
    [Flags]
    public enum PermissionListOptions
    {
        None = 0x0,
        OrganizeByGroup = 0x1,
        AllInformation = 0x2,
        Summary = 0x4,
        OnlyDangerous = 0x8,
        UsersVisible = 0x10,
    }

    public static class PermissionListOptionsEx
    {
        public static string GenerateString(this PermissionListOptions options)
        {
            string optionsString = string.Empty;
            if ((options & PermissionListOptions.OrganizeByGroup) != 0)
                throw new NotImplementedException("Must parse result");//optionsString += " -g";
            if ((options & PermissionListOptions.AllInformation) != 0)
                throw new NotImplementedException("Must parse result");//optionsString += " -f";
            if ((options & PermissionListOptions.Summary) != 0)
                throw new NotImplementedException("Must parse result"); //optionsString += " -s";
            if ((options & PermissionListOptions.OnlyDangerous) != 0)
                optionsString += " -d";
            if ((options & PermissionListOptions.UsersVisible) != 0)
                optionsString += " -u";

            return optionsString;
        }
    }
}
