using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAdb.Util
{
    public static class StringEx
    {
        public static string QuoteIfNeeded(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            if (s == "")
                return "\"\"";
            if (char.IsWhiteSpace(s[0]))
                return "\"" + s + "\"";
            if (char.IsWhiteSpace(s[s.Length - 1]))
                return "\"" + s + "\"";

            var containsDoubleQuote = false;
            var containsWhiteSpace = false;

            foreach (var @char in s)
            {
                if (@char == '"')
                {
                    containsDoubleQuote = true;
                }
                else if (char.IsWhiteSpace(@char))
                {
                    containsWhiteSpace = true;
                }

                if (containsDoubleQuote && containsWhiteSpace)
                    break;
            }

            if (containsDoubleQuote)
                return s;

            if (!containsWhiteSpace)
                return s;

            return "\"" + s + "\"";
        }

        public static string ExceptionQuoteIfNeeded(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            if (s == "")
                return "\"\"";

            var containsWhiteSpace = s.Any(char.IsWhiteSpace);
            var containsQuotes = s.Any(c => c == '"');

            if (s[0] == '"' || s[s.Length - 1] == '"')
                return s;


            if (containsQuotes)
            {
                return "'\"'" + s + "'\"\'";
            }
            if (containsWhiteSpace)
            {
                return "\"" + s + "\"";
            }

            return s;
        }

        public static string CsvQuoteIfNeeded(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            foreach (var @char in s)
            {
                switch (@char)
                {
                    case '"':
                    case ',':
                    case ';':
                        return "\"" + s.Replace("\"", "\"\"") + "\"";
                }
            }
            if (s.Contains(DefaultEnvironment.NewLine))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        public static bool IsMultiline(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            foreach (var @char in s)
            {
                switch (@char)
                {
                    case '\r':
                    case '\n':
                        return true;
                }
            }

            return false;
        }

        public static string NormalizeLineEndings(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            var newLine = DefaultEnvironment.NewLine;

            if (newLine == "\n")
            {
                return s.Replace("\r\n", "\n");
            }

            if (newLine == "\r\n")
            {
                var resultBuilder = new StringBuilder();

                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] != '\r' && s[i + 1] == '\n')
                    {
                        resultBuilder.Append(s[i] + "\r\n");
                        i++;
                        continue;
                    }
                    resultBuilder.Append(s[i]);
                }
                if (s.Length != 0)
                    resultBuilder.Append(s[s.Length - 1]);

                return resultBuilder.ToString();
            }

            var exception = new NotSupportedException("Environment.NewLine has unsupported value");
            exception.Data.Add("Environment.NewLine value", DefaultEnvironment.NewLine);
            throw exception;
        }

        public static string ShortenIfNeeded(this string s, int maxLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (s.Length > maxLength)
                return s.Substring(0, maxLength - 3) + "...";
            return s;
        }

        public static IEnumerable<string> ToLines(this string s)
        {
            return s.Split(new[] { DefaultEnvironment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string FromLines(this IEnumerable<string> lines)
        {
            var linesList = lines as IList<string> ?? lines.ToList();
            if (!linesList.Any())
                return string.Empty;

            return string.Join(DefaultEnvironment.NewLine, linesList.ToArray()).Trim();
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string ToNullable(this string text)
        {
            return string.IsNullOrEmpty(text) ? null : text;
        }

        public static int? ToNullableInt(this string text)
        {
            int value;
            if (int.TryParse(text, out value))
                return value;
            else return null;
        }

        public static long? ToNullableLong(this string text)
        {
            long value;
            if (long.TryParse(text, out value))
                return value;
            else return null;
        }

        public static string Quote(this string path)
        {
            return string.Format("\"{0}\"", path);
        }
    }
}
