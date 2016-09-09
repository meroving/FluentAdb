using System.Collections.Generic;

namespace FluentAdb.Process
{
    public class ProcessResult : IProcessResult
    {
        private readonly IProcess _process;
        private readonly IEnumerable<string> _output;
        public ProcessResult(IProcess process, IEnumerable<string> output)
        {
            _process = process;
            _output = output;
        }

        public IProcess Process
        {
            get { return _process; }
        }

        public IEnumerable<string> Output
        {
            get { return _output; }
        }
    }
}