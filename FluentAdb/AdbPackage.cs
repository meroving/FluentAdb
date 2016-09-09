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

            return lines.Select(l =>
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

        public Task Clear(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Enable(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Disable(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Grant(string permission, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Revoke(string permission, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<InstallLocation> GetInstallLocation(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task SetInstallLocation(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task TrimCaches(int freeSpace, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task CreateUser(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task RemoveUser(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMaxUsers(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
