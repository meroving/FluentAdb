namespace FluentAdb.Enums
{
    public enum InstallLocation
    {
        /// <summary>
        /// Lets system decide the best location
        /// </summary>
        Auto = 0,

        /// <summary>
        /// Installs on internal device storage
        /// </summary>
        Internal = 1,

        /// <summary>
        /// Installs on external media
        /// </summary>
        External = 2
    }
}
