using System;
using System.Linq;

namespace FluentAdb.Enums
{
    [Flags]
    public enum LogcatOptions
    {
        None = 0x0,
        ClearLogAndExit = 0x1,
        DumpAndExit = 0x2,
        PrintSizeAndExit = 0x4,
        DefaultSilent = 0x8
    }

    public enum LogBuffer
    {
        Main = 0,
        Radio,
        Events
    }

    public enum LogPriorityLevel
    {
        Verbose = 0,
        Debug,
        Info,
        Warning,
        Error,
        Fatal,
        Silent
    }

    public enum LogOutputFormat
    {
        Brief,
        Process,
        Tag,
        Raw,
        Time,
        ThreadTime,
        Long
    }

    public struct LogFilter
    {
        public const string ALL = "*";

        public LogPriorityLevel Level;
        public string Tag;

        public LogFilter(LogPriorityLevel level, string tag = ALL)
        {
            Level = level;
            Tag = tag;
        }

        public override string ToString()
        {
            string levelStr = Level.ToString().First().ToString();
            return string.Format("{0}:{1}", Tag, levelStr);
        }
    }


    public static class LogcatOptionsEx
    {
        public static string GenerateString(this LogcatOptions options)
        {
            string optionsString = string.Empty;
            if ((options & LogcatOptions.ClearLogAndExit) != 0)
                optionsString += " -c";
            if ((options & LogcatOptions.DumpAndExit) != 0)
                optionsString += " -d";
            if ((options & LogcatOptions.PrintSizeAndExit) != 0)
                optionsString += " -g";
            if ((options & LogcatOptions.DefaultSilent) != 0)
                optionsString += " -s";

            return optionsString;
        }
    }
}
