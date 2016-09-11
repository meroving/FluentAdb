using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IPackageManager
    {
        Task<IEnumerable<string>> GetPackages(string filter = "", PackageListOptions options = PackageListOptions.None, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<string>> GetPermissionGroups(CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<string>> GetPermissions(PermissionListOptions options = PermissionListOptions.None, string group = "", CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<OutUser>> GetUsers(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetPath(string package, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> Install(string path, InstallOptions options = InstallOptions.None, CancellationToken cancellationToken = default(CancellationToken));
        Task Uninstall(string package, UninstallOptions options = UninstallOptions.None, CancellationToken cancellationToken = default(CancellationToken));
        Task Clear(string package, CancellationToken cancellationToken = default(CancellationToken));
        Task Enable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken));
        Task Disable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken));
        Task DisableUser(string packageOrComponent, InUser user, CancellationToken cancellationToken = default(CancellationToken));
        Task Grant(string package, string permission, CancellationToken cancellationToken = default(CancellationToken));
        Task Revoke(string package, string permission, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstallLocation> GetInstallLocation(CancellationToken cancellationToken = default(CancellationToken));
        Task SetInstallLocation(InstallLocation installLocation, CancellationToken cancellationToken = default(CancellationToken));
        Task SetPermissionEnforced(string permission, bool enforced, CancellationToken cancellationToken = default(CancellationToken));
        Task TrimCaches(int freeSpace, CancellationToken cancellationToken = default(CancellationToken));
        Task CreateUser(string userName, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveUser(string userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> GetMaxUsers(CancellationToken cancellationToken = default(CancellationToken));
    }
}
