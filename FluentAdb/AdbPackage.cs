using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Interfaces;
using FluentAdb.Util;

namespace FluentAdb
{
    public partial class Adb : IPackageManager
    {
        public async Task<IEnumerable<string>> GetPackages(string filter = "", PackageListOptions options = PackageListOptions.None, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string filterString = filter ?? "";

            var lines = (await new Adb(this, "list packages {0} {1} {2}", options.GenerateString(), GetUserString(user), filterString)
                .RunAsync(timeout: 1000 * 10, cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines();

            return lines.Select(l => l.Substring("package:".Length));
        }

        public async Task<IEnumerable<string>> GetPermissionGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "list permission-groups").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines();

            return lines.Select(l => l.Substring("permission group:".Length));
        }

        public async Task<IEnumerable<string>> GetPermissions(PermissionListOptions options = PermissionListOptions.None, string group = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "list permissions {0} {1}", options.GenerateString(), group).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines();

            return lines.Select(l => l.Substring("permission:".Length));
        }

        public async Task<IEnumerable<OutUser>> GetUsers(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "list users").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines();

            return lines
                .Where(l=>l.Trim().StartsWith("UserInfo"))
                .Select(l =>
                {
                    var tokens = l.Split('{', ':', '}');
                    return new OutUser { Id = tokens[1], Name = tokens[2] };
                });
        }

        public async Task<string> GetPath(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "path {0}", package).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines().ToList();
            if (lines.Count <= 1)
                return string.Empty;
            return lines.ElementAt(1).Substring("package:".Length);
        }

        public async Task Uninstall(string package, UninstallOptions options = UninstallOptions.None, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "uninstall {0} {1}", options.GenerateString(), package).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task Clear(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "clear {0}", package).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task Enable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "enable {0}", packageOrComponent).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task Disable(string packageOrComponent, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "disable {0}", packageOrComponent).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task DisableUser(string packageOrComponent, InUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            await new Adb(this, "disable-user {0} {1}", user, packageOrComponent).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task Grant(string package, string permission, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "grant {0} {1}", package, permission).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task Revoke(string package, string permission, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "revoke {0} {1}", package, permission).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task<InstallLocation> GetInstallLocation(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "get-install-location").RunAsync(cancellationToken: cancellationToken)).ToLines().ToList();
            if (lines.Count < 1)
                return InstallLocation.Auto;

            int installLocationNum;
            if (!int.TryParse(lines[0].Trim().Substring(0, 1), out installLocationNum) || installLocationNum > 2)
            {
                return InstallLocation.Auto;
            }

            return (InstallLocation)installLocationNum;
        }

        public async Task SetInstallLocation(InstallLocation installLocation, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "set-install-location").RunAsync(cancellationToken: cancellationToken);
        }

        public async Task SetPermissionEnforced(string permission, bool enforced, CancellationToken cancellationToken = new CancellationToken())
        {
            await new Adb(this, "set-permission-enforced {0} {1}", permission, enforced.ToString().ToLower()).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task TrimCaches(int freeSpace, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "trim-caches {0}", freeSpace).RunAsync(cancellationToken: cancellationToken);
        }

        public async Task CreateUser(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var adb = new Adb(this, "create-user {0}", userName);
            var lines = (await adb.RunAsync(cancellationToken: cancellationToken)).ToLines().ToList();
            if (lines.Count < 1 || lines[0].StartsWith("Error", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AdbException("Error creating user", adb);
            }
        }

        public async Task RemoveUser(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var adb = new Adb(this, "remove-user {0}", userId);
            var lines = (await adb.RunAsync(cancellationToken: cancellationToken)).ToLines().ToList();
            if (lines.Count < 1 || lines[0].StartsWith("Error", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AdbException("Error removing user", adb);
            }
        }

        public async Task<int> GetMaxUsers(CancellationToken cancellationToken = default(CancellationToken))
        {
            var adb = new Adb(this, "get-max-users");
            var lines = (await adb.RunAsync(cancellationToken: cancellationToken)).ToLines().ToList();
            if (lines.Count < 1)
            {
                throw new AdbException(adb);
            }

            string maxUsersString = lines[0].Replace("Maximum supported users:", "").Trim();
            int maxUsers;
            if (!int.TryParse(maxUsersString, out maxUsers))
            {
                throw new AdbException("Invalid output format", adb);
            }

            return maxUsers;
        }
    }
}
