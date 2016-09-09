using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Interfaces;
using FluentAdb.Util;

namespace FluentAdb
{
    public partial class Adb : IShell
    {
        public IActivityManager ActivityManager
        {
            get
            {
                return new Adb(this, "am");
            }
        }

        public IPackageManager PackageManager
        {
            get
            {
                return new Adb(this, "pm");
            }
        }

        public IDumpSys DumpSys
        {
            get { return new Adb(this, "dumpsys"); }
        }

        public async Task<string> GetProperty(string property, CancellationToken cancellationToken = default(CancellationToken))
        {
            var output = await new Adb(this, "getprop {0}", property).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(output))
                return null;
            return output.Trim();
        }

        public async Task<Dictionary<string, string>> GetAllProperties(CancellationToken cancellationToken = default(CancellationToken))
        {
            var lines = (await new Adb(this, "getprop").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).ToLines();
            var dict = new Dictionary<string, string>();
            foreach (var l in lines)
            {
                int openBraceIndex1 = l.IndexOf('[');
                int length1 = l.IndexOf(']') - openBraceIndex1 - 1;
                if (length1 <= 0)
                    continue;
                string key = l.Substring(openBraceIndex1 + 1, length1);
                int openBraceIndex2 = l.LastIndexOf('[');
                int length2 = l.LastIndexOf(']') - openBraceIndex2 - 1;
                string val = length2 > 0 ? l.Substring(openBraceIndex2 + 1, length2) : "";
                dict.Add(key, val);
            }
            return dict;
        }

        public async Task<string> DiskStats(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "df").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> RunCommand(string command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, command).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }


        public async Task Input(string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "input text \'{0}\'", text).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task Input(KeyCode key, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "input keyevent {0}", ((int)key).ToString()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }


        public async Task<string> ListDirectory(string directory, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (directory.Last() != '/')
                directory += '/';
            var output = await new Adb(this, "ls {0}", directory.QuoteIfNeeded()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            if (output.Contains("No such file or directory"))
                return null;
            else
                return output;
        }

        public async Task MakeDirectory(string directory, CancellationToken cancellationToken = default(CancellationToken))
        {
            await new Adb(this, "mkdir {0}", directory.QuoteIfNeeded()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> Monkey(string package, int eventsCount = 1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "monkey -p {0} {1}", package, eventsCount.ToString()).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> NetworkConfiguration(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, "netcfg").RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
