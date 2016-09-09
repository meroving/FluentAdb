using System.Collections.Generic;

namespace FluentAdb.Process
{
    public interface IProcessResult
    {
        IProcess Process { get; }
        IEnumerable<string> Output { get; }
    }
}