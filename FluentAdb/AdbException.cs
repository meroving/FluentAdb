using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAdb.Interfaces;

namespace FluentAdb
{
    public class AdbException : Exception
    {
        public IAdb Adb { get; private set; }

        public AdbException(string message, IAdb adb)
            : base(message)
        {
            Adb = adb;
        }

        public AdbException(IAdb adb)
            : base("Fail to execute ADB command")
        {
            Adb = adb;
        }
    }
}
