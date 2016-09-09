using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Interfaces;

namespace FluentAdb
{
    public partial class Adb: IDumpSys
    {
        public async Task<string> DumpService(string service, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await new Adb(this, service).RunAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
