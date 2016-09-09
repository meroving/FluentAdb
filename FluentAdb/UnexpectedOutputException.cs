using System;

namespace FluentAdb
{
    public class UnexpectedOutputException : Exception
    {
        public string Output { get; private set; }
        public UnexpectedOutputException(string output)
            : base(
                string.Format("Unexpected output exception. Output was: {0}", output))
        {
            Output = output;
        }
    }
}
