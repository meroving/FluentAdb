using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Interfaces;

namespace FluentAdb
{
    public partial class Adb : IActivityManager
    {
        public async Task<bool> Start(Intent intent, StartOptions options = StartOptions.None, int repeatCount = -1, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string optionsString = string.Empty;
            if ((options & StartOptions.EnableDebugging) != 0)
                optionsString += " -D";
            if ((options & StartOptions.WaitForLaunchComplete) != 0)
                optionsString += " -W";
            if ((options & StartOptions.ForceStop) != 0)
                optionsString += " -S";

            var output = await new Adb(this, "start {0} {1} {2}", optionsString, GetUserString(user), intent.ToString()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            return !output.Contains("Error:");
        }

        public async Task StartService(Intent intent, InUser? user = null, bool log = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user.HasValue && user.Value.Equals(InUser.All))
                throw new InvalidOperationException("User parameter can't be All");

            await new Adb(this, "startservice {0} {1}", GetUserString(user), intent.ToString()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task ForceStop(string package, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "force-stop {0}", package).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task Kill(string package, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "kill {0} {1}", GetUserString(user), package).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task KillAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "kill-all").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> Broadcast(Intent intent, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "broadcast {0} {1}", GetUserString(user), intent.ToString()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
