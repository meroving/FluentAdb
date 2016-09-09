using System.Collections.Generic;
using System.Linq;
using FluentAdb.Util;

namespace FluentAdb
{
    public class Intent
    {
        private string _action;
        private string _dataUri;
        private string _mimeType;
        private string _category;
        private string _component;
        private string _package;
        private Dictionary<string, string> _stringExtra;
        private Dictionary<string, bool> _boolExtra;
        private Dictionary<string, int> _integerExtra;
        private Dictionary<string, long> _longExtra;
        private Dictionary<string, float> _floatExtra;

        public static Intent New
        {
            get { return new Intent(); }
        }

        public Intent With(string package)
        {
            _package = package;
            return this;
        }

        public Intent Action(string action)
        {
            _action = action;
            return this;
        }


        public Intent DataUri(string dataUri)
        {
            _dataUri = dataUri;
            return this;
        }

        public Intent MimeType(string mimeType)
        {
            _mimeType = mimeType;
            return this;
        }

        public Intent Category(string category)
        {
            _category = category;
            return this;
        }

        public Intent Component(string component)
        {
            _component = component;
            return this;
        }

        public Intent Extra(string key, string value)
        {
            if (_stringExtra == null)
                _stringExtra = new Dictionary<string, string>();

            _stringExtra.Add(key.ExceptionQuoteIfNeeded(), value.ExceptionQuoteIfNeeded());
            return this;
        }

        public Intent Extra(string key, bool value)
        {
            if (_boolExtra == null)
                _boolExtra = new Dictionary<string, bool>();
            _boolExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        public Intent Extra(string key, int value)
        {
            if (_integerExtra == null)
                _integerExtra = new Dictionary<string, int>();
            _integerExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        public Intent Extra(string key, long value)
        {
            if (_longExtra == null)
                _longExtra = new Dictionary<string, long>();
            _longExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        public Intent Extra(string key, float value)
        {
            if (_floatExtra == null)
                _floatExtra = new Dictionary<string, float>();
            _floatExtra.Add(key.ExceptionQuoteIfNeeded(), value);

            return this;
        }

        public override string ToString()
        {
            List<string> intent = new List<string>();
            if (_action != null)
                intent.Add(string.Format("-a {0}", _package != null ? _package + "." + _action : _action));

            if (_dataUri != null)
                intent.Add(string.Format("-d {0}", _dataUri));

            if (_mimeType != null)
                intent.Add(string.Format("-t {0}", _mimeType));

            if (_category != null)
                intent.Add(string.Format("-c {0}", _category));

            if (_component != null)
                intent.Add(string.Format("-n {0}", _package != null ? _package + "/." + _component : _component));

            if (_stringExtra != null)
                intent.AddRange(_stringExtra.Select(kv => string.Format("-e {0} {1}", kv.Key, kv.Value)));

            if (_boolExtra != null)
                intent.AddRange(_boolExtra.Select(kv => string.Format("--ez {0} {1}", kv.Key, kv.Value)));

            if (_integerExtra != null)
                intent.AddRange(_integerExtra.Select(kv => string.Format("--ei {0} {1}", kv.Key, kv.Value)));

            if (_longExtra != null)
                intent.AddRange(_longExtra.Select(kv => string.Format("--el {0} {1}", kv.Key, kv.Value)));

            if (_floatExtra != null)
                intent.AddRange(_floatExtra.Select(kv => string.Format("--ef {0} {1}", kv.Key, kv.Value)));

            return intent.Aggregate("", (acc, s) => acc + " " + s);
        }
    }
}
