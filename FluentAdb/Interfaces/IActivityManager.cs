using System.Threading;
using System.Threading.Tasks;
using FluentAdb.Enums;

namespace FluentAdb.Interfaces
{
    public interface IActivityManager
    {
        /// <summary>
        /// Start an Activity specified by intent
        /// </summary>
        /// <param name="intent">Intent for start activity</param>
        /// <param name="options">Start options</param>
        /// <param name="repeatCount">Number of times to repeat the activity launch. Prior to each repeat, the top activity will be finished</param>
        /// <param name="user">User to run as; if not specified, then run as the current user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Start(Intent intent, StartOptions options = StartOptions.None, int repeatCount = -1, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Start the Service specified by intent.
        /// </summary>
        /// <param name="intent">Intent for start service</param>
        /// <param name="user">User to run as. If not specified, then run as the current user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartService(Intent intent, InUser? user = null,  CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Force stop everything associated with package/>
        /// </summary>
        /// <param name="package">App's package name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ForceStop(string package, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Kill all processes associated with package. This command kills only processes that are safe to kill and that will not impact the user experience.
        /// </summary>
        /// <param name="package">App's package name</param>
        /// <param name="user">User whose processes to kill; all users if not specified</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Kill(string package, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Kill all background processes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task KillAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Issue a broadcast intent
        /// </summary>
        /// <param name="intent">Broadcast intent</param>
        /// <param name="user">Which user to send to; if not specified then send to all users</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> Broadcast(Intent intent, InUser? user = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
