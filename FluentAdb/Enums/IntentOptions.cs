using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAdb.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum IntentOptions
    {
        FLAG_NONE,
        /// <summary>
        /// If set, the recipient of this Intent will be granted permission to perform read operations on the URI in the Intent's data and any URIs specified in its ClipData. When applying to an Intent's ClipData, all URIs as well as recursive traversals through data or other ClipData in Intent items will be granted; only the grant flags of the top-level Intent are used.
        /// </summary>
        FLAG_GRANT_READ_URI_PERMISSION = 0x00000001,
        
        /// <summary>
        /// If set, the recipient of this Intent will be granted permission to perform write operations on the URI in the Intent's data and any URIs specified in its ClipData. When applying to an Intent's ClipData, all URIs as well as recursive traversals through data or other ClipData in Intent items will be granted; only the grant flags of the top-level Intent are used.
        /// </summary>
        FLAG_GRANT_WRITE_URI_PERMISSION = 0x00000002,

        /// <summary>
        /// A flag you can enable for debugging: when set, log messages will be printed during the resolution of this intent to show you what has been found to create the final resolved list.
        /// </summary>
        FLAG_DEBUG_LOG_RESOLUTION = 0x00000008,
        
        /// <summary>
        /// If set, this intent will not match any components in packages that are currently stopped. If this is not set, then the default behavior is to include such applications in the result.
        /// </summary>
        FLAG_EXCLUDE_STOPPED_PACKAGES = 0x00000010,
        
        /// <summary>
        /// If set, this intent will always match any components in packages that are currently stopped. This is the default behavior when FLAG_EXCLUDE_STOPPED_PACKAGES is not set. If both of these flags are set, this one wins (it allows overriding of exclude for places where the framework may automatically set the exclude flag).
        /// </summary>
        FLAG_INCLUDE_STOPPED_PACKAGES = 0x00000020,

        /// <summary>
        /// This flag is not normally set by application code, but set for you by the system as described in the launchMode documentation for the singleTask mode.
        /// </summary>
        FLAG_ACTIVITY_BROUGHT_TO_FRONT = 0x00400000,

        /// <summary>
        /// If set, and the activity being launched is already running in the current task, then instead of launching a new instance of that activity, all of the other activities on top of it will be closed and this Intent will be delivered to the (now on top) old activity as a new Intent.
        /// </summary>
        FLAG_ACTIVITY_CLEAR_TOP = 0x04000000,
        
        /// <summary>
        /// Performs identically to FLAG_ACTIVITY_NEW_DOCUMENT which should be used instead of this.
        /// </summary>
        [Obsolete]
        FLAG_ACTIVITY_CLEAR_WHEN_TASK_RESET = 0x00080000,
        
        /// <summary>
        /// If set, the new activity is not kept in the list of recently launched activities.
        /// </summary>
        FLAG_ACTIVITY_EXCLUDE_FROM_RECENTS = 0x00800000,

        /// <summary>
        /// This flag is not normally set by application code, but set for you by the system if this activity is being launched from history (longpress home key).
        /// </summary>
        FLAG_ACTIVITY_LAUNCHED_FROM_HISTORY = 0x00100000,

        /// <summary>
        /// This flag is used to create a new task and launch an activity into it. This flag is always paired with either FLAG_ACTIVITY_NEW_DOCUMENT or FLAG_ACTIVITY_NEW_TASK.
        /// </summary>
        FLAG_ACTIVITY_MULTIPLE_TASK = 0x08000000,

        /// <summary>
        /// If set in an Intent passed to Context.startActivity(), this flag will prevent the system from applying an activity transition animation to go to the next activity state. 
        /// </summary>
        FLAG_ACTIVITY_NO_ANIMATION = 0x00010000,

        /// <summary>
        /// If set, the new activity is not kept in the history stack. As soon as the user navigates away from it, the activity is finished.
        /// </summary>
        FLAG_ACTIVITY_NO_HISTORY = 0x40000000,

        /// <summary>
        /// If set, this flag will prevent the normal onUserLeaveHint() callback from occurring on the current frontmost activity before it is paused as the newly-started activity is brought to the front.
        /// </summary>
        FLAG_ACTIVITY_NO_USER_ACTION = 0x00040000,

        /// <summary>
        /// If set and this intent is being used to launch a new activity from an existing one, the current activity will not be counted as the top activity for deciding whether the new intent should be delivered to the top instead of starting a new one. The previous activity will be used as the top, with the assumption being that the current activity will finish itself immediately.
        /// </summary>
        FLAG_ACTIVITY_PREVIOUS_IS_TOP = 0x01000000,

        /// <summary>
        /// If set in an Intent passed to StartActivity, this flag will cause the launched activity to be brought to the front of its task's history stack if it is already running.
        /// </summary>
        FLAG_ACTIVITY_REORDER_TO_FRONT = 0x00020000,

        /// <summary>
        /// If set, and this activity is either being started in a new task or bringing to the top an existing task, then it will be launched as the front door of the task. This will result in the application of any affinities needed to have that task in the proper state (either moving activities to or from it), or simply resetting that task to its initial state if needed.
        /// </summary>
        FLAG_ACTIVITY_RESET_TASK_IF_NEEDED = 0x00200000,

        /// <summary>
        /// If set, the activity will not be launched if it is already running at the top of the history stack.
        /// </summary>
        FLAG_ACTIVITY_SINGLE_TOP = 0x20000000,

        /// <summary>
        /// If set in an Intent passed to Start method, this flag will cause any existing task that would be associated with the activity to be cleared before the activity is started. That is, the activity becomes the new root of an otherwise empty task, and any old activities are finished.
        /// </summary>
        FLAG_ACTIVITY_CLEAR_TASK = 0x00008000,

        /// <summary>
        /// If set in an Intent passed to Start method, this flag will cause a newly launching task to be placed on top of the current home activity task (if there is one). That is, pressing back from the task will always return the user to home even if that was not the last activity they saw. 
        /// </summary>
        FLAG_ACTIVITY_TASK_ON_HOME = 0x00004000,

        /// <summary>
        /// If set, when sending a broadcast only registered receivers will be called -- no BroadcastReceiver components will be launched.
        /// </summary>
        FLAG_RECEIVER_REGISTERED_ONLY = 0x40000000,

        /// <summary>
        /// If set, when sending a broadcast the new broadcast will replace any existing pending broadcast that matches it. Matching is defined by Intent.filterEquals returning true for the intents of the two broadcasts. When a match is found, the new broadcast (and receivers associated with it) will replace the existing one in the pending broadcast list, remaining at the same position in the list.
        /// This flag is most typically used with sticky broadcasts, which only care about delivering the most recent values of the broadcast to their receivers.
        /// </summary>
        FLAG_RECEIVER_REPLACE_PENDING = 0x20000000,
    }
}
