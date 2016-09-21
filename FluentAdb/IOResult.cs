using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAdb
{
    /// <summary>
    /// Result of I/O operations, like Pull and Push
    /// </summary>
    public class IOResult
    {
        /// <summary>
        /// Is I/O operation suceeded
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message, if operation failed
        /// </summary>
        public string Error { get; set; } 
    }
}
