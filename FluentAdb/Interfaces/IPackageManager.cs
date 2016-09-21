using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IPackageManager
    {
        /// <summary>
        /// Get all packages, optionally only those whose package name contains the text in filter.
        /// </summary>
        /// <param name="filter">Filter for package names</param>
        /// <param name="options">Options</param>
        /// <param name="user">The user space to query.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPackages(string filter = "", PackageListOptions options = PackageListOptions.None, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Get all known permission groups
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionGroups(CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Get all known permissions, optionally only those in  group
        /// </summary>
        /// <param name="options">Options</param>
        /// <param name="group">Permissions group</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissions(PermissionListOptions options = PermissionListOptions.None, string group = "", CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Get all users on the system.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<OutUser>> GetUsers(CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Get the path to the APK of the given package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetPath(string package, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Installs a package (specified by path) to the system.
        /// </summary>
        /// <param name="path">Package path</param>
        /// <param name="options">Options</param>
        /// <param name="installer">Installer for apk</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> Install(string path, InstallOptions options = InstallOptions.None, string installer = null, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Removes a package from the system.
        /// </summary>
        /// <param name="package">Application package</param>
        /// <param name="options">Options</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Uninstall(string package, UninstallOptions options = UninstallOptions.None, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Deletes all data associated with a package.
        /// </summary>
        /// <param name="package">Application package</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Clear(string package, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enable the given package or component (written as "package/class").
        /// </summary>
        /// <param name="packageOrComponent">Application package or component</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Enable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Disable the given package or component (written as "package/class").
        /// </summary>
        /// <param name="packageOrComponent">Application package or component</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Disable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Disable the given package or component (written as "package/class") for specified user
        /// </summary>
        /// <param name="packageOrComponent">Application package or component</param>
        /// <param name="user">The user to disable.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DisableUser(string packageOrComponent, InUser user, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Grant a permission to an app. On devices running Android 6.0 (API level 23) and higher, may be any permission declared in the app manifest. On devices running Android 5.1 (API level 22) and lower, must be an optional permission defined by the app.
        /// </summary>
        /// <param name="package">Application package</param>
        /// <param name="permission">Permission</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Grant(string package, string permission, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Revoke a permission from an app. On devices running Android 6.0 (API level 23) and higher, may be any permission declared in the app manifest. On devices running Android 5.1 (API level 22) and lower, must be an optional permission defined by the app.
        /// </summary>
        /// <param name="package">Application package</param>
        /// <param name="permission">Permission</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Revoke(string package, string permission, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the current install location.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Install location</returns>
        Task<InstallLocation> GetInstallLocation(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Changes the default install location.
        /// </summary>
        /// <param name="installLocation">Install location</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetInstallLocation(InstallLocation installLocation, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Specifies whether the given permission should be enforced.
        /// </summary>
        /// <param name="permission">Permission</param>
        /// <param name="enforced">Is enforced</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetPermissionEnforced(string permission, bool enforced, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Trim cache files to reach the given free space.
        /// </summary>
        /// <param name="freeSpace">Free space to reach</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task TrimCaches(int freeSpace, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a new user with the given userName, printing the new user identifier of the user.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateUser(string userName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove the user with the given userId, deleting all data associated with that user
        /// </summary>
        /// <param name="userId">User identificator</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveUser(string userId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get the maximum number of users supported by the device.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetMaxUsers(CancellationToken cancellationToken = default(CancellationToken));
    }
}
